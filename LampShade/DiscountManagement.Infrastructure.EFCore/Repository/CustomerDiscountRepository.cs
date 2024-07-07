using System.Linq.Expressions;
using _0_Framework.Infrastructure;
using Azure.Core;
using DiscountManagement.Application.Contracts.CustomerDiscount;
using DiscountManagement.Domain.CustomerDiscountAgg;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Infrastructure.EFCore;

namespace DiscountManagement.Infrastructure.EFCore.Repository;

public class CustomerDiscountRepository : RepositoryBase<long, CustomerDiscount>, ICustomerDiscountRepository
{
    private readonly DiscountContext _context;
    private readonly ShopContext _shopContext;
    public CustomerDiscountRepository(DiscountContext context, ShopContext shopContext) : base(context)
    {
        _context = context;
        _shopContext = shopContext;
    }


    public List<CustomerDiscountViewModel> Search(CustomerDiscountSearchModel searchModel)
    {
        var products = _shopContext.Products.Select(x => new { x.Id, x.Name }).ToList();
        var query = _context.CustomerDiscounts.Select(x => new CustomerDiscountViewModel()
        {
            Id = x.Id,
            DiscountRate = x.DiscountRate,
            ProductId = x.ProductId,
            StartDate = x.StartDate.ToString(),
            StartDateR = x.StartDate,
            EndDate = x.EndDate.ToString(),
            EndDateR = x.EndDate,
            Reason = x.Reason
        }).AsNoTracking();
        if (searchModel.ProductId > 0)
            query = query.Where(x => x.ProductId == searchModel.ProductId);
        
        if (!string.IsNullOrWhiteSpace(searchModel.StartDate))
        {
            var startDate = DateTime.Now;
            query = query.Where(x => x.StartDateR > startDate);
        }

        if (!string.IsNullOrWhiteSpace(searchModel.EndDate))
        {
            var endDate = DateTime.Now;
            query = query.Where(x => x.EndDateR < endDate);
        }

        var discounts = query.OrderByDescending(x => x.Id).ToList();
        discounts. ForEach(discount =>
            discount.Product = products.FirstOrDefault(x => x.Id == discount.ProductId) ? .Name);
        
        return discounts;
    }

    public EditCustomerDiscount GetDetails(long id)
    {
        return _context.CustomerDiscounts.Select(x => new EditCustomerDiscount()
        {
            Id = x.Id,
            DiscountRate = x.DiscountRate,
            ProductId = x.ProductId,
            Reason = x.Reason,
            StartDate = x.StartDate,
            EndDate = x.EndDate
        }).FirstOrDefault(x => x.Id == id);
    }
}