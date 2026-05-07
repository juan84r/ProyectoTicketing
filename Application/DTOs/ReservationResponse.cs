namespace Application.DTOs;
// DTO para la respuesta de la creación de una reserva
public class ReservationResponse
{
    public bool Success { get; set; }

     public ReservationResult Result { get; set; }

    public Guid? ReservationId { get; set; }
}