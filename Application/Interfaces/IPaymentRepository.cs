using Domain.Entities;

namespace Application.Interfaces;

public interface IPaymentRepository
{
    Task<bool> ConfirmPaymentAsync(Guid reservationId);
}