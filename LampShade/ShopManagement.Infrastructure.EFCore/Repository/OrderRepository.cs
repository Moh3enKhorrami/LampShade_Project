using System.Linq.Expressions;
using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Domain.OrderAgg;

namespace ShopManagement.Infrastructure.EFCore.Repository;

public class OrderRepository : RepositoryBase<long, Order>, IOrderRepository
{
    private readonly ShopContext _context;
    public OrderRepository(ShopContext context) : base(context)
    {
        _context = context;
    }

    public double GetAmountBy(long id)
    {
        throw new NotImplementedException();
    }
}