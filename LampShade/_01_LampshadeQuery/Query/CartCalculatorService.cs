using _01_LampshadeQuery.Contracts;
using DiscountManagement.Domain.CustomerDiscountAgg;
using DiscountManagement.Infrastructure.EFCore;
using InventoryManagement.Domain.InventoryAgg;
using ShopManagement.Application.Contracts.Order;

namespace _01_LampshadeQuery.Query;

public class CartCalculatorService : ICartCalculatorService
{
    private readonly DiscountContext _discountContext;
    public CartCalculatorService(DiscountContext discountContext)
    {
        _discountContext = discountContext;
    }

    public Cart ComputeCart(List<CartItem> cartItems)
    {
        var cart = new Cart();
        var discount = _discountContext.CustomerDiscounts
            .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now )
            .Select(x => new {x.DiscountRate, x.ProductId })
            .ToList();
        foreach (var cartItem in cartItems)
        {
            var customerDiscount = discount
                .FirstOrDefault(x => x.ProductId == cartItem.Id);
            if (customerDiscount != null)
                cartItem.DiscountRate = customerDiscount.DiscountRate;
            cartItem.DiscountAmount = ((cartItem.TotalPrice * cartItem.DiscountRate) / 100);
            cartItem.ItemPayAmount = cartItem.TotalPrice - cartItem.DiscountAmount;
            cart.Add(cartItem);
        }
        return cart;
    }
}