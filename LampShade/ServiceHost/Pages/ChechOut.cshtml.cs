using _01_LampshadeQuery.Contracts;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nancy.Json;
using ShopManagement.Application.Contracts.Order;

namespace ServiceHost.Pages;

public class ChechOut : PageModel
{
    public Cart Cart;
    public const string CookieName = "cart-items";
    private readonly ICartCalculatorService _cartCalculatorService;

    public ChechOut(ICartCalculatorService calculatorService)
    {
        Cart = new Cart();
        _cartCalculatorService = calculatorService;
    }

    public void OnGet()
    {
        var serializer = new JavaScriptSerializer();
        var value = Request.Cookies[CookieName];
        var cartItems = serializer.Deserialize<List<CartItem>>(value);
        foreach (var item in cartItems)
            item.CalculateTotalItemprice();
        var Cart = _cartCalculatorService.ComputeCart(cartItems);
    }
}