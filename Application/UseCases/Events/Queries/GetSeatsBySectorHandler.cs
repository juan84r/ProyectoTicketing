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

    public async Task<IEnumerable<SeatResponse>> HandleAsync(int sectorId)
{
    // 1. Buscamos el evento (usamos el ID 1 por ahora como seed)
    var eventData = await _eventRepository.GetEventByIdAsync(1);
    
    // 2. Buscamos el sector dentro de ese evento
    var sector = eventData?.Sectors.FirstOrDefault(s => s.Id == sectorId);
    
    // 3. VALIDACIÓN PROFESIONAL
    if (sector == null) 
    {
        throw new NotFoundException($"No se encontró el sector con ID {sectorId} para este evento.");
    }

    // 4. Mapeamos los asientos a DTOs de respuesta ORDENADOS
    return sector.Seats
        .OrderBy(s => s.SeatNumber) // <--- ESTO ES LO QUE FALTA
        .Select(s => new SeatResponse(
            s.Id, 
            s.RowIdentifier, 
            s.SeatNumber, 
            s.Status
        ));
}
}