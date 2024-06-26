using System.ComponentModel.DataAnnotations;
using ShopManagement.Application.Contracts.Product;

namespace ShopManagement.Application.Contracts.ProductPicture;

public class CreateProductPicture
{
    [Range(1,1000), Required]
    public long ProductId { get; set; }
    
    [MaxLength(100), Required]
    public string Picture { get;  set; }
    
    [MaxLength(500), Required]
    public string PictureAlt { get; set; }
    
    [MaxLength(500), Required]
    public string PictureTitle { get;  set; }
    
    public List<ProductViewModel> Products { get; set; }
}