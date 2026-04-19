using Application.DTOs;
using Application.Interfaces;

namespace Application.UseCases.Events.Queries;

public class GetEventsHandler
{
    private readonly IEventRepository _eventRepository;

    public GetEventsHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<IEnumerable<EventResponse>> HandleAsync()
    {
        var events = await _eventRepository.GetAllEventsAsync();
        
        // Convertimos las entidades de base de datos a DTOs para la API
        return events.Select(e => new EventResponse(
            e.Id, 
            e.Name, 
            e.EventDate, 
            e.Venue, 
            e.Status
        ));
    }
}