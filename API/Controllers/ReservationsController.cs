using Microsoft.AspNetCore.Mvc;
using Application.UseCases.Reservations;
using Application.DTOs;

namespace API.Controllers;

[ApiController]
[Route("api/v1/reservations")]
public class ReservationsController : ControllerBase
{
    private readonly CreateReservationHandler _handler;

    public ReservationsController(CreateReservationHandler handler)
    {
        _handler = handler;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateReservationRequest request)
    {
        var result = await _handler.Handle(request);

        if (!result)
            return BadRequest("No se pudo reservar el asiento");

        return Ok("Reserva exitosa");
    }
}