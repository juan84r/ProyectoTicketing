using Application.Interfaces;
using Domain.Entities;
using Application.UseCases.Reservations;

public class CreateReservationHandler
{
    private readonly ISeatRepository _seatRepository;
    private readonly IReservationRepository _reservationRepository;
    
    //private readonly IAuditRepository _auditRepository;

    public CreateReservationHandler(
        ISeatRepository seatRepository,
        IReservationRepository reservationRepository)
    {
        _seatRepository = seatRepository;
        _reservationRepository = reservationRepository;
        //_auditRepository = auditRepository;
       
    }

    public async Task<bool> Handle(CreateReservationRequest request)
    {
        foreach (var seatId in request.SeatIds)
        {
            var seat = await _seatRepository.GetByIdAsync(seatId);

            if (seat == null || seat.Status != "Available")
                return false;

            seat.Status = "Reserved";
            await _seatRepository.UpdateAsync(seat);
            

            var reservation = new Reservation
            {
                Id = Guid.NewGuid(),
                SeatId = seatId,
                UserId = request.UserId,
                ReservedAt = DateTime.UtcNow
            };

            await _reservationRepository.AddAsync(reservation);

            var log = new AuditLog
            {
                Action = "Seat Reserved",
                User = request.UserId.ToString(),
                Resource = seatId.ToString(),
                Timestamp = DateTime.UtcNow
                
            };
            //await _auditRepository.AddAsync(log);

            
        }

        return true;
    }
}