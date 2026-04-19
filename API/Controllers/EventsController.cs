using Application.UseCases.Events.Queries;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/v1/[controller]")] // La ruta será: api/v1/events
public class EventsController : ControllerBase
{
    private readonly GetEventsHandler _getEventsHandler;
    private readonly GetSeatsBySectorHandler _getSeatsHandler;

    // Inyectamos ambos Handlers a través del constructor
    public EventsController(
        GetEventsHandler getEventsHandler, 
        GetSeatsBySectorHandler getSeatsHandler)
    {
        _getEventsHandler = getEventsHandler;
        _getSeatsHandler = getSeatsHandler;
    }

    // Endpoint para listar todos los eventos (Catálogo)
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _getEventsHandler.HandleAsync();
        return Ok(result);
    }

    // Endpoint para ver los asientos de un sector (Mapa de asientos)
    // Ejemplo: GET api/v1/events/1/seats
    [HttpGet("{sectorId}/seats")]
    public async Task<IActionResult> GetSeats(int sectorId)
    {
        var result = await _getSeatsHandler.HandleAsync(sectorId);
        return Ok(result);
    }
}