using System;
namespace ShopManagement.Domain.ProductCategoryAgg
{
	public interface IProductCategoryRepository
	{
		void Create(ProductCategory entity);
		ProductCategory GetBy(long Id);
		List<ProductCategory> GetAll();

	}
}

