using System;
using _0_Framework.Application;
using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.Domain.ProductCategoryAgg;

namespace ShopManagement.Application
{
	public class ProductCategoryApplication : IProductCategoryApplication
	{

        private readonly IProductCategoryRepository _productCategoryRepository;

        public ProductCategoryApplication(IProductCategoryRepository productCategoryRepository)
        {
            _productCategoryRepository = productCategoryRepository;
        }


        public OperationResult Create(CreateProductCategory command)
        {
            var operation = new OperationResult();
            if(_productCategoryRepository.Exists(command.Name))
            {
                return operation.Failed("It is not possible to register a duplicate record. Try again");
            }
            var ProductCategory = new ProductCategory(command.Name, command.Description,
                command.Picture, command.PictureAlt, command.PictureTitle, command.KeyWords,
                command.MetaDescription, command.Slug);
        }

        public OperationResult Edit(EditProductCategory command)
        {
            throw new NotImplementedException();
        }

        public ProductCategory GetDetails(long id)
        {
            throw new NotImplementedException();
        }

        public List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel)
        {
            throw new NotImplementedException();
        }
    }
}

