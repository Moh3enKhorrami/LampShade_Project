using _01_LampshadeQuery.Contracts.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nancy.Json;
using ShopManagement.Application.Contracts.Order;

namespace ServiceHost.Pages;

public class CartModel : PageModel
{
    public List<CartItem> CartItems;
    public const string CookieName = "cart-items";
    private readonly IProductQuery _productQuery;

    public CartModel(IProductQuery productQuery)
    {
        _productQuery = productQuery;
        CartItems = new List<CartItem>();
    }

    public void OnGet()
    {
        var serializer = new JavaScriptSerializer(); // Nuget Nancy - convert Cookie
        var value = Request.Cookies[CookieName];
        var cartItems = serializer.Deserialize<List<CartItem>>(value);
        if (string.IsNullOrEmpty(value))
        {
            CartItems = new List<CartItem>();
            return;
        }
        foreach (var item in cartItems)
            item.CalculateTotalItemprice();

        CartItems = _productQuery.CheckInventoryStatus(cartItems);
    }

    public IActionResult OnGetRemoveFromCart(long id)
    {
        var serializer = new JavaScriptSerializer();
        var value = Request.Cookies[CookieName];
        Response.Cookies.Delete(CookieName);
        var cartItems = serializer.Deserialize<List<CartItem>>(value);
        var itemToRemove = cartItems.FirstOrDefault(x => x.Id == id);
        cartItems.Remove(itemToRemove);
        var options = new CookieOptions{Expires = DateTime.Now.AddDays(2)};
        Response.Cookies.Append(CookieName, serializer.Serialize(cartItems), options);
        return RedirectToPage("/Cart");
    }

    public IActionResult OnGetGoToCheckOut()
    {
        var serializer = new JavaScriptSerializer();
        var value = Request.Cookies[CookieName];
        var cartItems = serializer.Deserialize<List<CartItem>>(value);
        foreach (var item in cartItems)
            item.CalculateTotalItemprice();

        CartItems = _productQuery.CheckInventoryStatus(cartItems);
        if (CartItems.Any(x => !x.IsInStock))
            return RedirectToPage("/Cart");
        return RedirectToPage("/ChechOut");
    }
}