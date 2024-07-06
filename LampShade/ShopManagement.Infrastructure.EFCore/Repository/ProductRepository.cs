using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Domain.ProductAgg;

namespace ShopManagement.Infrastructure.EFCore.Repository;

public class ProductRepository : RepositoryBase<long, Product>, IProductRepository
{
    private readonly ShopContext _context;
    
    public ProductRepository(ShopContext context) : base(context)
    {
        _context = context;
    }

    public List<ProductViewModel> Search(ProductSearchModel searchModel)
    {
        var query = _context.Products.Include(x => x.Category)
                                     .Select(x => new ProductViewModel()
        {
            Id = x.Id,
            Name = x.Name,
            Category = x.Category.Name,
            CategoryId = x.CategoryId,
            Code = x.Code,
            Picture = x.Picture,
            CreationDate = x.CreationDate.ToString()
            
        });
        if (!string.IsNullOrWhiteSpace(searchModel.Name))
            query = query.Where(x => x.Name.Contains(searchModel.Name));

        if (!string.IsNullOrWhiteSpace(searchModel.Code))
            query = query.Where(x => x.Code.Contains(searchModel.Code));

        if (searchModel.CategoryId != 0)
            query = query.Where(x => x.CategoryId == searchModel.CategoryId);
        return query.OrderByDescending(x => x.Id).ToList();
    }

    public EditProduct GetDetails(long id)
    {
        return _context.Products.Select(x => new EditProduct()
        {
            Id = x.Id,
            Name = x.Name,
            Code = x.Code,
            Slug = x.Slug,
            CategoryId = x.CategoryId,
            Description = x.Description,
            Keyworks = x.Keyworks,
            MetaDescription = x.MetaDescription,
            
            PictureAlt = x.PictureAlt,
            PictureTitle = x.PictureTitle,
            ShortDescription = x.ShortDescription
        }).FirstOrDefault(x => x.Id == id) ?? throw new InvalidOperationException();
    }

    public List<ProductViewModel> GetProducts()
    {
        return _context.Products.Select(x => new ProductViewModel()
        {
            Id = x.Id,
            Name = x.Name
        }).ToList();
    }
}