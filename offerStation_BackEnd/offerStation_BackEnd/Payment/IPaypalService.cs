using offerStation.Core.Dtos;

namespace offerStation.API.Payment
{
    public interface IPaypalService
    {
        ApiResponse PaymentWithPaypal(PaymentDto userData);
    }
}