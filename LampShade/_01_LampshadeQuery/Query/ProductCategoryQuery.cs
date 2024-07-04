using System.Data;
using System.Diagnostics.Tracing;
using _01_LampshadeQuery.Contracts.Product;
using _01_LampshadeQuery.Contracts.ProductCategory;
using DiscountManagement.Application.Contracts.CustomerDiscount;
using DiscountManagement.Infrastructure.EFCore;
using InventoryMangement.Infrastructure.EFCorel;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Infrastructure.EFCore;

namespace _01_LampshadeQuery.Query;

public class ProductCategoryQuery : IProductCategoryQuery
{
    private readonly ShopContext _context;
    private readonly InventoryContext _inventoryContext;
    private readonly DiscountContext _discountContext;
    public ProductCategoryQuery(ShopContext context, InventoryContext inventoryContext, DiscountContext discountContext)
    {
        _discountContext = discountContext;
        _inventoryContext = inventoryContext;
        _context = context;
    }

    public ProductCategoryQueryModel GetProductCategoryWithProductsBy(string slug)
    {
        var inventory = _inventoryContext.Inventories
            .Select((x => new { x.ProductId, x.UnitPrice })).ToList();

        var discounts = _discountContext.CustomerDiscounts
            .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
            .Select(x => new { x.ProductId, x.DiscountRate, x.EndDate }).ToList();

        var category = _context.ProductCategories
            .Include(x => x.Products)
            .ThenInclude(x => x.Category)
            .Select(x => new ProductCategoryQueryModel()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Keyworks = x.Keywords,
                MetaDescription = x.MetaDescription,
                Slug = x.Slug,
                Products = MapProducts(x.Products)
            }).FirstOrDefault(x => x.Slug == slug);

        foreach (var product in category.Products)
        {
            var productInventory = inventory
                .FirstOrDefault(x => x.ProductId == product.Id);
            if (productInventory != null)
            {
                var discount = discounts.FirstOrDefault(x 
                    => x.ProductId == product.Id && x.DiscountRate > 0);
                var price = productInventory.UnitPrice;
                product.Price = price;
                if (discount != null)
                {
                    var discountRate = discount.DiscountRate;
                    product.DiscountExpireDate = discount.EndDate.ToString(); 
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

        return category;
    }

    
    
    
    
    public List<ProductCategoryQueryModel> GetProductCategories()
    {
        return _context.ProductCategories.Select(x => new ProductCategoryQueryModel()
        {
            Id = x.Id,
            Name = x.Name,
            Picture = x.Picture,
            PictureAlt = x.PictureAlt,
            PictureTitle = x.PictureTitle,
            Slug = x.Slug
        }).ToList();
        
    }
    
    public List<ProductCategoryQueryModel> GetProductCategoriesWithProducts()
    {
        var inventory = _inventoryContext.Inventories
            .Select(x => new {x.ProductId, x.UnitPrice }).ToList();

        var discounts = _discountContext.CustomerDiscounts
            .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
            .Select(x => new { x.DiscountRate, x.ProductId }).ToList();
        
        
        
        var categories = _context.ProductCategories
            .Include(x => x.Products)
            .ThenInclude(x => x.Category)
            .Select(x => new ProductCategoryQueryModel()
            {
                Id = x.Id,
                Name = x.Name,
                Products = MapProducts(x.Products)
            }).ToList();

        
        foreach (var category in categories)
        {
            foreach (var product in category.Products)
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
        }

        return categories;
    }

    private static List<ProductQueryModel> MapProducts(List<Product> products)
    {
        return products.Select(product => new ProductQueryModel()
        {
            Id = product.Id,
            Category = product.Category.Name,
            Name = product.Name,
            Picture = product.Picture,
            PictureAlt = product.PictureAlt,
            PictureTitle = product.PictureTitle,
            Slug = product.Slug,
        }).ToList();
        
    }
}