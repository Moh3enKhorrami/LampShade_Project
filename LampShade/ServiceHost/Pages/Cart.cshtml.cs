using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nancy.Json;
using ShopManagement.Application.Contracts.Order;

namespace ServiceHost.Pages;

public class Cart : PageModel
{
    public List<CartItem> CartItems;
    public const string CookieName = "cart-items";
    public void OnGet()
    {
        var serializer = new JavaScriptSerializer(); // Nuget Nancy
        var value = Request.Cookies[CookieName];
        CartItems = serializer.Deserialize<List<CartItem>>(value);
        foreach (var item in CartItems)
        {
            item.TotalPrice = item.UnitPrice * item.Count;
        }
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
}