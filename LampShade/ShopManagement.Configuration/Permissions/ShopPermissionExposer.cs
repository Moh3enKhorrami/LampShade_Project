using _0_Framework.Infrastructure;

namespace ShopManagement.Configuration.Permissions;

public class ShopPermissionExposer : IPermissionExposer
{
    public Dictionary<string, List<PermissionDto>> Expose()
    {
        return new Dictionary<string, List<PermissionDto>>
        {
            {
                "Product" , new List<PermissionDto> //IProductApplication
                {
                    new PermissionDto(ShopPermissions.ListProducts, "ListProducts"),
                    new PermissionDto(ShopPermissions.SearchProducts,"SearchProducts"),
                    new PermissionDto(ShopPermissions.CreateProduct,"CreateProducts"),
                    new PermissionDto(ShopPermissions.EditProduct,"EditProducts"),
                }
            },
            {
                "ProductCategory", new List<PermissionDto> //IProductCategoryApplication
                {
                    new PermissionDto(ShopPermissions.SearchProductCategories,"SearchProductCategories"),
                    new PermissionDto(ShopPermissions.ListProductCategories,"ListProductCategories"),
                    new PermissionDto(ShopPermissions.CreateProductCategory,"CreateProductCategory"),
                    new PermissionDto(ShopPermissions.EditProductCategory,"EditProductCategory"),
                }
            },
        };
    }
}