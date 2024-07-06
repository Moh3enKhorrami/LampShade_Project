using _0_Framework.Domain;
using ShopManagement.Application.Contracts.ProductPicture;

namespace ShopManagement.Domain.PictureAgg;

public interface IProductPictureRepository : IRepository<long, ProductPicture>
{
    List<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel);
    EditProductPicture GetDetails(long id);
    ProductPicture GetWithProductAndCategory(long id);

}