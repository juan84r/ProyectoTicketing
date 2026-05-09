using Application.Interfaces;
using Domain.Entities;
using Application.UseCases.Reservations;
using Domain.Constants;
using Application.DTOs;

public class CreateReservationHandler
{
    private readonly ISeatRepository _seatRepository;
    private readonly IReservationRepository _reservationRepository;
    private readonly IUserRepository _userRepository;
    private readonly IAuditRepository _auditRepository;

    // Validamos primero todos los datos para evitar modificaciones parciales en la base de datos
    public CreateReservationHandler(
        ISeatRepository seatRepository,
        IReservationRepository reservationRepository,
        IAuditRepository auditRepository,
        IUserRepository userRepository)
    {
        _seatRepository = seatRepository;
        _reservationRepository = reservationRepository;
        _auditRepository = auditRepository;
        _userRepository = userRepository;
    }

    public async Task<ReservationResponse> Handle(CreateReservationRequest request)
    {
        try
        {
            var seats = new List<Seat>();

            foreach (var seatId in request.SeatIds)
            {
                var seat = await _seatRepository.GetByIdAsync(seatId);

                if (seat == null)
                {
                    return new ReservationResponse
                    {
                        Result = ReservationResult.SeatNotFound
                    };
                }

                if (seat.Status != SeatStatus.Available)
                {
                    return new ReservationResponse
                    {
                        Result = ReservationResult.SeatAlreadyReserved
                    };
                }

                seats.Add(seat);
            }

            var user = await _userRepository.GetByIdAsync(request.UserId);

            if (user == null)
            {
                return new ReservationResponse
                {
                    Result = ReservationResult.UserNotFound
                };
            }

            //guardamos TODOS los reservationIds
            var reservationIds = new List<Guid>();

            foreach (var seat in seats)
            {
                seat.Status = SeatStatus.Reserved;

                await _seatRepository.UpdateAsync(seat);

                var reservation = new Reservation
                {
                    Id = Guid.NewGuid(),
                    SeatId = seat.Id,
                    UserId = request.UserId,
                    ReservedAt = DateTime.UtcNow,
                    Status = ReservationStatus.Pending
                };

                // Guardamos cada ID
                reservationIds.Add(reservation.Id);

                await _reservationRepository.AddAsync(reservation);

                var log = new AuditLog
                {
                    Action = "Seat Reserved",
                    User = user.Email,
                    Resource = $"Asiento {seat.SeatNumber}",
                    Timestamp = DateTime.UtcNow
                };

                await _auditRepository.AddAsync(log);
            }

            return new ReservationResponse
            {
                Result = ReservationResult.Success,
                ReservationIds = reservationIds
            };
        }
        catch (Exception)
        {
            return new ReservationResponse
            {
                Result = ReservationResult.SeatAlreadyReserved
            };
        }
    }
}