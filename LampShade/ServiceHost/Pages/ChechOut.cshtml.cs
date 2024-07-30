using _01_LampshadeQuery.Contracts;
using _01_LampshadeQuery.Contracts.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nancy.Json;
using ShopManagement.Application.Contracts.Order;

namespace ServiceHost.Pages;

public class ChechOut : PageModel
{
    public Cart Cart;
    public const string CookieName = "cart-items";
    private readonly ICartService _cartService;
    private readonly IProductQuery _productQuery;
    private readonly ICartCalculatorService _cartCalculatorService;
    private readonly IOrderApplication _orderApplication;

    public ChechOut(ICartCalculatorService calculatorService, ICartService cartService, IProductQuery productQuery, IOrderApplication orderApplication)
    {
        _productQuery = productQuery;
        _cartService = cartService;
        Cart = new Cart();
        _cartCalculatorService = calculatorService;
        _orderApplication = orderApplication;
    }

    public void OnGet()
    {
        var serializer = new JavaScriptSerializer();
        var value = Request.Cookies[CookieName];
        var cartItems = serializer.Deserialize<List<CartItem>>(value);
        foreach (var item in cartItems)
            item.CalculateTotalItemprice();
        Cart = _cartCalculatorService.ComputeCart(cartItems);
        _cartService.Set(Cart);
    }

    public IActionResult OnGetPay()
    {
        var cart = _cartService.Get();
        var result = _productQuery.CheckInventoryStatus(cart.Items);
        if (result.Any(x => x.IsInStock == false))
            return RedirectToPage("/Cart");

        var orderId = _orderApplication.PlaceOrder(cart);
        
        // Send Payment
        
        return RedirectToPage("/CheckOut");
    }
}