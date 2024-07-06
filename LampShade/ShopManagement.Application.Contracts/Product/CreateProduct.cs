
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using ShopManagement.Application.Contracts.ProductCategory;

namespace ShopManagement.Application.Contracts.Product;

public class CreateProduct
{
    [MaxLength(255), Required]
    public string Name { get;  set; }
    
    [MaxLength(15), Required]
    public string Code { get;  set; }
    
    [MaxLength(550), Required]
    public string ShortDescription { get;  set; }
    
    public string Description { get;  set; }
    
    public IFormFile Picture { get;  set; }
    
    [MaxLength(255)]
    public string PictureAlt { get;  set; }
    
    [MaxLength(500)]
    public string PictureTitle { get;  set; }
   
    [Range(1, 100000), Required]
    public long   CategoryId { get;  set; }
    
    [MaxLength(500), Required]
    public string Slug { get;  set; }
    
    [MaxLength(100), Required]
    public string Keyworks { get;  set; }
    
    [MaxLength(150), Required]
    public string MetaDescription { get;  set; }
    
    public List<ProductCategoryViewModel> Categories { get; set; }
}