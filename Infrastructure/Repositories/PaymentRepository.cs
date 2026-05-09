using Application.Interfaces;
using Application.UseCases.Reservations;
using Domain.Constants;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Repositories;

public class PaymentRepository : IPaymentRepository
{
    private readonly AppDbContext _context;

    public PaymentRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<PaymentResult> ConfirmPaymentAsync(Guid reservationId)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            var reservation = await _context.Reservations
                .FirstOrDefaultAsync(r => r.Id == reservationId);

            if (reservation == null)
                return PaymentResult.ReservationNotFound;

            var seat = await _context.Seats
                .FirstOrDefaultAsync(s => s.Id == reservation.SeatId);

            if (seat == null)
                return PaymentResult.ReservationNotFound;

            // Evita doble pago
            if (reservation.Status == ReservationStatus.Paid)
                return PaymentResult.AlreadyPaid;

            // Evita vender dos veces
            if (seat.Status == SeatStatus.Sold)
                return PaymentResult.AlreadyPaid;

            // Cambiamos estados
            reservation.Status = ReservationStatus.Paid;
            seat.Status = SeatStatus.Sold;

            // Auditoría
            _context.AuditLogs.Add(new AuditLog
            {
                Action = "PAYMENT_CONFIRMED",
                User = reservation.UserId.ToString(),
                Resource = $"Asiento {seat.SeatNumber}",
                Timestamp = DateTime.UtcNow
            });

            await _context.SaveChangesAsync();

            await transaction.CommitAsync();

            return PaymentResult.Success;
        }
        catch
        {
            await transaction.RollbackAsync();

            return PaymentResult.AlreadyPaid;
        }
    }
}