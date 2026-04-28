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

    return result switch
    {
        ReservationResult.SeatNotFound =>
            NotFound("El asiento no existe"),

        ReservationResult.SeatAlreadyReserved =>
            Conflict("El asiento ya está reservado"),

        ReservationResult.UserNotFound =>
            NotFound("El usuario no existe"),

        ReservationResult.Success =>
            Created("", "Reserva realizada correctamente"),

        _ => StatusCode(500, "Error inesperado")
    };
}
}