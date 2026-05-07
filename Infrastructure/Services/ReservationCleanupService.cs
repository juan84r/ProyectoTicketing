using Domain.Constants;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Infrastructure.Services;
// Servicio en segundo plano para limpiar reservas pendientes que hayan expirado
public class ReservationCleanupService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public ReservationCleanupService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = _serviceProvider.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            var expiredReservations = await context.Reservations
                .Where(r =>
                    r.Status == ReservationStatus.Pending &&
                    r.ReservedAt <= DateTime.UtcNow.AddMinutes(-5))
                .ToListAsync();

            foreach (var reservation in expiredReservations)
            {
                var seat = await context.Seats
                    .FirstOrDefaultAsync(s => s.Id == reservation.SeatId);

                if (seat != null)
                {
                    seat.Status = SeatStatus.Available;
                }

                reservation.Status = ReservationStatus.Expired;

                context.AuditLogs.Add(new AuditLog
                {
                    Action = "AUTO_RELEASE",
                    User = "SYSTEM",
                    Resource = $"Asiento {seat?.SeatNumber}",
                    Timestamp = DateTime.UtcNow
                });
            }

            await context.SaveChangesAsync();

            await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
        }
    }
}