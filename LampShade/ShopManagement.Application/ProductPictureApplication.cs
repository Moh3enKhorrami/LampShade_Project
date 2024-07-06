using _0_Framework.Application;
using ShopManagement.Application.Contracts.ProductPicture;
using ShopManagement.Domain.PictureAgg;
using ShopManagement.Domain.ProductAgg;

namespace ShopManagement.Application;

public class ProductPictureApplication : IProductPictureApplication
{
    private readonly IFileUpLoader _fileUpLoader;
    private readonly IProductRepository  _productRepository;
    private readonly IProductPictureRepository _productPictureRepository;
    public ProductPictureApplication(IProductPictureRepository productPictureRepository, IProductRepository productRepository, IFileUpLoader fileUpLoader)
    {
        _productPictureRepository = productPictureRepository;
        _productRepository = productRepository;
        _fileUpLoader = fileUpLoader;
    }


    public OperationResult Create(CreateProductPicture command)
    {
        var operation = new OperationResult();
        // if (_productPictureRepository.Exists(x => x.Picture == command.Picture && x.ProductId == command.ProductId))
        //     return operation.Failed(ApplicationMessages.Dulpicated);
        var product = _productRepository.GetProductWithCategory(command.ProductId);
        var path = $"{product.Category.Slug}//{product.Slug}";
        var picturePath = _fileUpLoader.UpLoad(command.Picture, path);
        
        var productPicture =
            new ProductPicture(command.ProductId, picturePath, command.PictureAlt, command.PictureTitle);
        _productPictureRepository.Create(productPicture);
        _productPictureRepository.SaveChanges();
        return operation.Succedded();
    }

    public OperationResult Edit(EditProductPicture command)
    {
        var operation = new OperationResult();
        var productPicture = _productPictureRepository.GetWithProductAndCategory(command.Id);
        if (productPicture == null)
            operation.Failed(ApplicationMessages.RecordNotFound);
        
        // if (_productPictureRepository.Exists(x =>
        //         x.Picture == command.Picture && x.ProductId == command.ProductId && x.Id == command.Id))
        //     operation.Failed(ApplicationMessages.DuplicatedRecord);
        
        var path = $"{productPicture.Product.Category.Slug}//{productPicture.Product.Slug}";
        var picturePath = _fileUpLoader.UpLoad(command.Picture, path);
        
        productPicture.Edit(command.ProductId, picturePath,command.PictureAlt, command.PictureTitle);
        _productPictureRepository.SaveChanges();
        return operation.Succedded();
    }

    public EditProductPicture GetDetails(long id)
    {
        return _productPictureRepository.GetDetails(id);
    }

    public OperationResult Remove(long id)
    {
        var operation = new OperationResult();
        var productPicture = _productPictureRepository.Get(id);
        if (productPicture == null)
            operation.Failed(ApplicationMessages.RecordNotFound);
        productPicture.Remove();
        _productPictureRepository.SaveChanges();
        return operation.Succedded();
    }

    public OperationResult Restore(long id)
    {
        var operation = new OperationResult();
        var productPicture = _productPictureRepository.Get(id);
        if (productPicture == null)
            operation.Failed(ApplicationMessages.RecordNotFound);
        
        productPicture.Restore();
        _productPictureRepository.SaveChanges();
        return operation.Succedded();
    }

    public List<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel)
    {
        return _productPictureRepository.Search(searchModel);
    }
}