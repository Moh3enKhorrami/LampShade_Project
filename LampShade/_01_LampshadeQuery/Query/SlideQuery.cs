using _01_LampshadeQuery.Contracts.Slide;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Infrastructure.EFCore;

namespace _01_LampshadeQuery.Query;

public class SlideQuery : ISlideQuery
{
    public SlideQuery(ShopContext context)
    {
        _context = context;
    }

    private readonly ShopContext _context;
    public List<SlideQueryModel> GetSlides()
    {
        return _context.Slides.Where(x => x.IsRemoved == false)
                              .Select(x => new SlideQueryModel()
        {
            Picture = x.Picture,
            PictureTitle = x.PictureTitle, 
            PictureAlt = x.PictureAlt,
            Heading = x.Heading,
            BtnText = x.BtnText,
            Text = x.Text,
            Title = x.Title,
            Link = x.Link
        }).AsNoTracking().ToList();
    }
}