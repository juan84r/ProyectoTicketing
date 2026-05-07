namespace Domain.Constants;
// Definimos los posibles estados de una reserva como constantes para evitar errores
public static class ReservationStatus
{
    public const string Pending = "Pendiente";
    public const string Paid = "Pagado";
    public const string Expired = "Vencido";
}