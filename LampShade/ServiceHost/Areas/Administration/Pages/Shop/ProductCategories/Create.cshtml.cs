﻿using _0_Framework.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.Configuration.Permissions;

namespace ServiceHost.Areas.Administration.Pages.Shop.ProductCategories
{
	public class CreateModel : PageModel
    {
        public CreateProductCategory Command;
        private readonly IProductCategoryApplication _productCategoryApplication;
        
        public CreateModel(IProductCategoryApplication productCategoryApplication)
        {
            _productCategoryApplication = productCategoryApplication;
            Command = new CreateProductCategory();
        }
        
        [NeedsPermission(ShopPermissions.CreateProductCategory)]
        public void OnGet()
        {
        }
        
        [NeedsPermission(ShopPermissions.CreateProductCategory)]
        public IActionResult OnPostCreate(CreateProductCategory Command)
        {
            var result = _productCategoryApplication.Create(Command);
            if (result.IsSuccedded)
                return RedirectToPage("./Index");
            else
            {
                return Page();
            }
        }
    }
}
