using _0_Framework.Domain;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductCategory;

namespace ShopManagement.Domain.ProductAgg;

public interface IProductRepository : IRepository<long, Product>
{
    List<ProductViewModel> Search(ProductSearchModel searchModel);
    EditProduct GetDetails(long id);
    List<ProductViewModel> GetProducts();


}