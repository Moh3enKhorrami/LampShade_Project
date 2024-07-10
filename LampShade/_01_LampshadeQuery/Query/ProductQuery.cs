using _01_LampshadeQuery.Contracts.Product;
using CommentManagement.Infrastructure.EFCore;
using DiscountManagement.Infrastructure.EFCore;
using InventoryMangement.Infrastructure.EFCorel;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Domain.PictureAgg;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Infrastructure.EFCore;

namespace _01_LampshadeQuery.Query;

public class ProductQuery : IProductQuery
{
    private readonly ShopContext _context;
    private readonly InventoryContext _inventoryContext;
    private readonly DiscountContext _discountContext;
    private readonly CommentContext _commentContext;
    public ProductQuery(ShopContext context, InventoryContext inventoryContext, DiscountContext discountContext, CommentContext commentContext)
    {
        _context = context;
        _inventoryContext = inventoryContext;
        _discountContext = discountContext;
        _commentContext = commentContext;
    }
    
    public List<ProductQueryModel> GetLatestArrivals()
    {
        var inventory = _inventoryContext.Inventories
            .Select(x => new {x.ProductId, x.UnitPrice }).ToList();

        var discounts = _discountContext.CustomerDiscounts
            .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
            .Select(x => new { x.DiscountRate, x.ProductId}).ToList();
        
        var products = _context.Products
            .Include(x => x.Category)
            .Select(product => new ProductQueryModel
            {
                Id = product.Id,
                Category = product.Category.Name,
                CategorySlug = product.Category.Slug,
                Name = product.Name,
                Picture = product.Picture,
                PictureAlt = product.PictureAlt,
                PictureTitle = product.PictureTitle,
                Slug = product.Slug
            }).AsNoTracking().OrderByDescending(x => x.Id).Take(6).ToList();
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

    public List<ProductQueryModel> Search(string value)
    {
        var inventory = _inventoryContext.Inventories.Select(x 
            => new { x.ProductId, x.UnitPrice }).ToList();
        var discounts = _discountContext.CustomerDiscounts
            .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
            .Select(x => new { x.DiscountRate, x.ProductId, x.EndDate }).ToList();
        var products = _context.Products
            .Include(x => x.Category)
            .Select(x => new ProductQueryModel()
        {
            Id = x.Id,
            Name = x.Name,
            Category = x.Category.Name,
            Picture = x.Picture,
            PictureTitle = x.PictureTitle,
            PictureAlt = x.PictureAlt,
            ShortDescription = x.ShortDescription,
            Slug = x.Slug
            
        }).AsNoTracking().ToList();

        if (!string.IsNullOrWhiteSpace(value))
        {
            products = (List<ProductQueryModel>)products.Where(x 
                => x.Name.Contains(value) || x.ShortDescription.Contains(value));
            
        }
        var product = products.OrderByDescending(x => x.Id).ToList();
        
        foreach (var item in products)
        {
            var productInventory = inventory
                .FirstOrDefault(x => x.ProductId == item.Id);
            if (productInventory != null)
            {
                var price = productInventory.UnitPrice;
                item.Price = price;
                var discount = discounts
                    .FirstOrDefault(x => x.ProductId == item.Id);
                if (discount != null)
                {
                    var discountRate = discount.DiscountRate;
                    item.DiscountExpireDate = discount.EndDate.ToString(); 
                    item.DiscountRate = discountRate;
                    item.HasDiscount = true;
                    var discountAmount = Math.Round((price * discountRate) / 100);
                    item.PriceWithDiscount = (price - discountAmount).ToString();
                }
            }
        }

        return products;
    }

    public ProductQueryModel GetDetails(string slug)
    {
        var inventory = _inventoryContext.Inventories
            .Select(x => new {x.ProductId, x.UnitPrice, x.InStock }).ToList();

        
        var discounts = _discountContext.CustomerDiscounts
            .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
            .Select(x => new { x.DiscountRate, x.ProductId, x.EndDate }).ToList();
        
        var product = _context.Products
            .Include(x => x.Category)
            .Include(x => x.ProductPictures)
            .Select(product => new ProductQueryModel
            {
                Id = product.Id,
                Category = product.Category.Name,
                CategorySlug = product.Category.Slug,
                Name = product.Name,
                Picture = product.Picture,
                PictureAlt = product.PictureAlt,
                PictureTitle = product.PictureTitle,
                Slug = product.Slug,
                Code = product.Code,
                Description = product.Description,
                Keywords = product.Keyworks,
                MetaDescription = product.MetaDescription,
                ShortDescription = product.ShortDescription,
                Pictures = MapProductPictures(product.ProductPictures)
            }).AsNoTracking().FirstOrDefault(x=> x.Slug == slug);

        if (product == null)
        {
            return new ProductQueryModel();
        }
        
        var productInventory = inventory.FirstOrDefault(x 
            => x.ProductId == product.Id);
        if (productInventory != null)
        {
            product.InStock = productInventory.InStock;
            var discount = discounts.FirstOrDefault(x 
                => x.ProductId == product.Id && x.DiscountRate > 0);
            var price = productInventory.UnitPrice;

            product.Price = price;
            if (discount != null)
            {
                var discountRate = discount.DiscountRate;
                product.DiscountRate = discountRate;
                product.DiscountExpireDate = discount.EndDate.ToString();
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
        product.Commands = _commentContext.Comments
            .Where(x => x.Type == CommentTypes.Product)
            .Where(x => x.OwnerRecordId == product.Id)
            .Select(x => new CommentQueryModel()
            {
                Id = x.Id,
                Message = x.Message,
                Name = x.Name
            }).AsNoTracking().OrderByDescending(x => x.Id).ToList();
        
        return product;
    }
    

    private static List<ProductPictureQueryModel> MapProductPictures(List<ProductPicture> picture)
    {
        return picture
            .Select(x => new ProductPictureQueryModel()
            {
                IsRemoved = x.IsRemoved,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                ProductId = x.ProductId
            }).Where(x => !x.IsRemoved).ToList();
    }
}