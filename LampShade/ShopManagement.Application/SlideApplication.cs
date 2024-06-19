using _0_Framework.Application;
using _0_Framework.Domain;
using ShopManagement.Application.Contracts.Slide;
using ShopManagement.Domain.SlideAgg;

namespace ShopManagement.Application;

public class SlideApplication : ISlideApplication
{
    private readonly ISlideRepository _slideRepository;

    public SlideApplication(ISlideRepository slideRepository)
    {
        _slideRepository = slideRepository;
    }

    public OperationResult Create(CreateSlide comman)
    {
        var operation = new OperationResult();
        var slide = new Slide(comman.Picture, comman.PictureTitle, comman.PictureAlt,
            comman.Heading, comman.Title, comman.Text, comman.BtnText, comman.Link);
        _slideRepository.Create(slide);
        _slideRepository.SaveChanges();
        return operation.Succedded();
    }

    public OperationResult Edit(EditSlide command)
    {
        
        var operation = new OperationResult();
        var slide = _slideRepository.Get(command.Id);
        if (slide == null)
            return operation.Failed(ApplicationMessages.RecordNotFound);

        slide.Edit(command.Picture, command.PictureAlt, command.PictureTitle,
            command.Heading, command.Title, command.Text, command.BtnText, command.Link);
        _slideRepository.SaveChanges();
        return operation.Succedded();
    }

    public EditSlide GetDetails(long id)
    {
        return _slideRepository.GetDetails(id);
    }

    public OperationResult Remove(long id)
    {
        var operation = new OperationResult();
        var slide = _slideRepository.Get(id);
        if (slide == null)
            operation.Failed(ApplicationMessages.RecordNotFound);
        slide.Remove();
        _slideRepository.SaveChanges();
        return operation.Succedded();
    }

    public OperationResult Restore(long id)
    {
        var operation = new OperationResult();
        var slide = _slideRepository.Get(id);
        if (slide == null)
            operation.Failed(ApplicationMessages.RecordNotFound);
        slide.Restore();
        _slideRepository.SaveChanges();
        return operation.Succedded();
    }

    public List<SlideViewModel> GetList()
    {
        return _slideRepository.GetList();
    }
}