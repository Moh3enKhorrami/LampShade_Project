using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;

namespace DiscountManagement.Application.Contracts.CustomerDiscount;

public class DefineCustomerDiscount
{
    public long ProductId { get; set; }
    public int DiscountRate { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Reason { get; set; }
    public List<ProductViewModel> Product { get; set; }
}