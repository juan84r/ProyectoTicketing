using Domain.Entities;

namespace Application.Interfaces;

public interface IEventRepository
{
    Task<IEnumerable<Event>> GetAllEventsAsync();
    Task<Event?> GetEventByIdAsync(int id);
}