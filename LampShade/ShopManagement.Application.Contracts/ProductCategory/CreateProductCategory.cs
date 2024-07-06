using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace ShopManagement.Application.Contracts.ProductCategory
{
    public class CreateProductCategory
	{
        [Required,MaxLength(255)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get;  set; }
        
        public IFormFile Picture { get;  set; }
        
        [MaxLength(255)]
        public string PictureAlt { get;  set; }
        
        [MaxLength(500)]
        public string PictureTitle { get;  set; }
        
        [MaxLength(80)]
        public string Keywords { get;  set; }
        
        [Required, MaxLength(150)]
        public string MetaDescription { get;  set; }
        
        [Required, MaxLength(300)]
        public string Slug { get;  set; }
    }
}

