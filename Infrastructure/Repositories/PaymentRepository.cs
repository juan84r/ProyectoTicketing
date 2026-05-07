using Application.Interfaces;
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

    public async Task<bool> ConfirmPaymentAsync(Guid reservationId)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            var reservation = await _context.Reservations
                .FirstOrDefaultAsync(r => r.Id == reservationId);

            if (reservation == null)
                return false;

            var seat = await _context.Seats
                .FirstOrDefaultAsync(s => s.Id == reservation.SeatId);

            if (seat == null)
                return false;

            if (reservation.Status == ReservationStatus.Paid)
                return false;

            if (seat.Status == SeatStatus.Sold)
                return false;

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

            return true;
        }
        catch
        {
            await transaction.RollbackAsync();
            return false;
        }
    }
}