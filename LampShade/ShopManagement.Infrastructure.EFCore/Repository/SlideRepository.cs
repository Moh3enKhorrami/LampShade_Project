using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.Slide;
using ShopManagement.Domain.SlideAgg;

namespace ShopManagement.Infrastructure.EFCore.Repository;

public class SlideRepository : RepositoryBase<long, Slide>, ISlideRepository
{
    private readonly ShopContext _context;
    public SlideRepository(ShopContext context) : base(context)
    {
        _context = context;
    }

    public EditSlide GetDetails(long id)
    {
        return _context.
    }

    public List<SlideViewModel> GetList()
    {
        throw new NotImplementedException();
    }
}