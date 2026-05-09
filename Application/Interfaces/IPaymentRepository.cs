using Domain.Entities;
using Application.UseCases.Reservations;

namespace Application.Interfaces;

public interface IPaymentRepository
{
    Task<PaymentResult> ConfirmPaymentAsync(Guid reservationId);
}