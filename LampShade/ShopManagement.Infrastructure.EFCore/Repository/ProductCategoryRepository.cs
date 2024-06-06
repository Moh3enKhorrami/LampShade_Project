using System;
using System.Linq.Expressions;
using _0_Framework.Infrastructure;
using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.Domain.ProductCategoryAgg;

namespace ShopManagement.Infrastructure.EFCore.Repository
{
	public class ProductCategoryRepository : RepositoryBase<long, ProductCategory>, IProductCategoryRepository
	{
        private readonly ShopContext _Context;

        public ProductCategoryRepository(ShopContext Context) : base(Context)
        {
            _Context = Context;
        }

        public void Create(ProductCategory entity)
        {
             _Context.ProductCategories.Add(entity);
        }

        public bool Exists(Expression<Func<ProductCategory, bool>> expression)
        {
            return _Context.ProductCategories.Any(expression);
        }

        public ProductCategory Get(long id)
        {
            return _Context.ProductCategories.Find(id);
        }

        public List<ProductCategory> GetAll()
        {
            return _Context.ProductCategories.ToList();
        }

        public EditProductCategory GetDetails(long id)
        {
            return _Context.ProductCategories.Select(x => new EditProductCategory()
            {
                Id = x.Id,
                Description = x.Description,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Keywords = x.Keywords,
                MetaDescription= x.MetaDescription,
                Slug = x.Slug


            }).FirstOrDefault(x => x.Id == id);
        }

        public void SaveChanges()
        {
            _Context.SaveChanges();
        }

        public List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel) //?
        {
            var query = _Context.ProductCategories.Select(x => new ProductCategoryViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Picture = x.Picture,
                CreationDate = x.CreationDate.ToString()
            });

            if (!string.IsNullOrWhiteSpace(searchModel.Name))
                query = query.Where(x => x.Name.Contains(searchModel.Name));

            return query.OrderByDescending(x => x.Id).ToList();


        }
    }
}

