namespace ShopManagement.Application.Contracts.Order;

public class CartItem
{
    public long Id { get; set; }
    public string Name { get; set; }
    public double UnitPrice { get; set; }
    public string Picture { get; set; }
    public int Count { get; set; }
    public double TotalPrice { get; set; }
    public bool IsInStock { get; set; }
    public int DiscountRate { get; set; }
    public double DiscountAmount { get; set; }
    public double ItemPayAmount { get; set; }

    public CartItem()
    {
        TotalPrice = UnitPrice * Count;
    }
    public void CalculateTotalItemprice()
    {
        TotalPrice = UnitPrice * Count;
    }
}