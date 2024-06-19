using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace ShopManagement.Application.Contracts.Slide;

public class CreateSlide
{
    [MaxLength(1000), Required]
    public string Picture { get; set; }
    
    [MaxLength(500), Required]
    public string PictureTitle { get; set; }
    
    [MaxLength(500), Required]
    public string PictureAlt { get; set; }
    
    [MaxLength(255), Required]
    public string Heading { get; set; }
    
    [MaxLength(255)]
    public string Title { get; set; }
    
    [MaxLength(255)]
    public string Text { get; set; }
    
    [MaxLength(50), Required]
    public string BtnText { get; set; }
    
    public string Link { get; set; }
    
}