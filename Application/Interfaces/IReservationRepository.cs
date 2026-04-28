using Domain.Entities;

namespace Application.Interfaces;

public interface IReservationRepository
{
    Task AddAsync(Reservation reservation);
}