using Application.Interfaces;

namespace Application.UseCases.Reservations;

public class ConfirmPaymentHandler
{
    private readonly IPaymentRepository _paymentRepository;

    public ConfirmPaymentHandler(IPaymentRepository paymentRepository)
    {
        _paymentRepository = paymentRepository;
    }

    public async Task<PaymentResult> Handle(Guid reservationId)
    {
        return await _paymentRepository
            .ConfirmPaymentAsync(reservationId);
    }
}