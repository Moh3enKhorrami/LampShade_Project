using _01_LampshadeQuery.Contracts.Product;
using DiscountManagement.Infrastructure.EFCore;
using InventoryMangement.Infrastructure.EFCorel;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Infrastructure.EFCore;

namespace _01_LampshadeQuery.Query;

public class ProductQuery : IProductQuery
{
    private readonly ShopContext _context;
    private readonly InventoryContext _inventoryContext;
    private readonly DiscountContext _discountContext;
    public ProductQuery(ShopContext context, InventoryContext inventoryContext, DiscountContext discountContext)
    {
        _context = context;
        _inventoryContext = inventoryContext;
        _discountContext = discountContext;
    }
    
    public List<ProductQueryModel> GetLatestArrivals()
    {
        var inventory = _inventoryContext.Inventories
            .Select(x => new {x.ProductId, x.UnitPrice }).ToList();

        var discounts = _discountContext.CustomerDiscounts
            .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
            .Select(x => new { x.DiscountRate, x.ProductId }).ToList();
        
        var products = _context.Products
            .Include(x => x.Category)
            .Select(product => new ProductQueryModel
            {
                Id = product.Id,
                Category = product.Category.Name,
                Name = product.Name,
                Picture = product.Picture,
                PictureAlt = product.PictureAlt,
                PictureTitle = product.PictureTitle,
                Slug = product.Slug
            }).OrderByDescending(x => x.Id).Take(6).ToList();
        foreach (var product in products)
        {
            var productInventory = inventory.FirstOrDefault(x 
                => x.ProductId == product.Id);
            if (productInventory != null)
            {
                var discount = discounts.FirstOrDefault(x 
                    => x.ProductId == product.Id && x.DiscountRate > 0);
                var price = productInventory.UnitPrice;

                product.Price = price;
                if (discount != null)
                {
                    var discountRate = discount.DiscountRate;
                    product.DiscountRate = discountRate;
                    product.HasDiscount = true;
                    var discountAmount = Math.Round((price * discountRate) / 100);
                    product.PriceWithDiscount = (price - discountAmount).ToString();
                }
                else
                {
                    product.HasDiscount = false; 
                    product.PriceWithDiscount = price.ToString(); 
                }
            }
        }
        return products;
    }
}