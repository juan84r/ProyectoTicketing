using Microsoft.AspNetCore.Mvc;
using Application.UseCases.Reservations;
using Application.DTOs;

namespace API.Controllers;

[ApiController]
[Route("api/v1/reservations")]
public class ReservationsController : ControllerBase
{
    private readonly CreateReservationHandler _handler;
    private readonly ConfirmPaymentHandler _paymentHandler;

    public ReservationsController(
        CreateReservationHandler handler,
        ConfirmPaymentHandler paymentHandler)
    {
        _handler = handler;
        _paymentHandler = paymentHandler;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateReservationRequest request)
    {
        var response = await _handler.Handle(request);

        return response.Result switch
        {
            // Se mapea el resultado del caso de uso a códigos HTTP
            ReservationResult.SeatNotFound =>
                NotFound("El asiento no existe"),

            ReservationResult.SeatAlreadyReserved =>
                Conflict("El asiento ya está reservado"),

            ReservationResult.UserNotFound =>
                NotFound("El usuario no existe"),

            ReservationResult.Success =>
                Created("", response),

            _ => StatusCode(500, "Error inesperado")
        };
    }

    [HttpPost("confirm")]
    public async Task<IActionResult> ConfirmPayment(ConfirmPaymentRequest request)
    {
        var success = await _paymentHandler.Handle(request.ReservationId);

        if (!success)
            return BadRequest("No se pudo confirmar el pago");

        return Ok("Pago confirmado correctamente");
    }
}