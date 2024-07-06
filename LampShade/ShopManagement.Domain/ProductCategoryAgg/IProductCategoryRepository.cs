﻿using System.Linq.Expressions;
using _0_Framework.Domain;
using ShopManagement.Application.Contracts.ProductCategory;

namespace ShopManagement.Domain.ProductCategoryAgg
{
    public interface IProductCategoryRepository : IRepository<long, ProductCategory>
	{
		//void Create(ProductCategory entity);
		//ProductCategory Get(long id);
		//List<ProductCategory> GetAll();
		//bool Exists(Expression<Func<ProductCategory, bool>> expression);
		//void SaveChanges();
		List<ProductCategoryViewModel> GetProductCategories();
		EditProductCategory GetDetails(long id);
		string GetSlugById(long id);
		List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel);
    }
}

