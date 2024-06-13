using System;
using System.ComponentModel.DataAnnotations;

namespace ShopManagement.Application.Contracts.ProductCategory
{
    public class CreateProductCategory
	{
        [Required]
        [MaxLength (255)]
        public string Name { get; set; }
        
        [Required]
        public string Description { get;  set; }
        public string Picture { get;  set; }
        public string PictureAlt { get;  set; }
        public string PictureTitle { get;  set; }
        public string Keywords { get;  set; }
        
        [Required]
        public string MetaDescription { get;  set; }
        
        [Required]
        public string Slug { get;  set; }
    }
}

