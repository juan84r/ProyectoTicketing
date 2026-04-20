using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Exceptions; // IMPORTANTE: Agregamos esto

namespace Application.UseCases.Events.Queries;

public class GetSeatsBySectorHandler
{
    private readonly IEventRepository _eventRepository;

    public GetSeatsBySectorHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<IEnumerable<SeatResponse>?> HandleAsync(int sectorId)
{
    // 1. Buscamos el evento
    var eventData = await _eventRepository.GetEventByIdAsync(1);
    
    // 2. Buscamos el sector
    var sector = eventData?.Sectors.FirstOrDefault(s => s.Id == sectorId);
    
    // 3. CAMBIO CLAVE: Si no existe, devolvemos null prolijamente
    if (sector == null) 
    {
        return null; 
    }

    // 4. Mapeamos y devolvemos los asientos
    return sector.Seats
        .OrderBy(s => s.SeatNumber) 
        .Select(s => new SeatResponse(
            s.Id, 
            s.RowIdentifier, 
            s.SeatNumber, 
            s.Status
        ));
}
}