using ShopManagement.Application.Contracts.Order;

namespace _01_LampshadeQuery.Contracts;

public interface ICartCalculatorService // CartCheckout
{
    Cart ComputeCart(List<CartItem> cartItems);
}