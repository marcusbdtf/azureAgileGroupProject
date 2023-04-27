using Stripe.Checkout;
using TeamKville.Server.Data.DataModels;
using TeamKville.Shared.Dto;


namespace TeamKville.Server.Data.Repositories.Interfaces
{
    public interface IPaymentService
    {
        Session CreateCheckoutSession(List<CartItemDto> cartItems);
    }

}
