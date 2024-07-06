using _0_Framework.Application;
using _0_Framework.Domain;
using ShopManagement.Application.Contracts.Slide;
using ShopManagement.Domain.SlideAgg;

namespace ShopManagement.Application;

public class SlideApplication : ISlideApplication
{
    private readonly IFileUpLoader _fileUpLoader;
    private readonly ISlideRepository _slideRepository;

    public SlideApplication(ISlideRepository slideRepository, IFileUpLoader fileUpLoader)
    {
        _slideRepository = slideRepository;
        _fileUpLoader = fileUpLoader;
    }

    public OperationResult Create(CreateSlide comman)
    {
        var operation = new OperationResult();
        var pictureName =_fileUpLoader.UpLoad(comman.Picture, "slides");
        var slide = new Slide(pictureName, comman.PictureTitle, comman.PictureAlt,
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
        var pictureName =_fileUpLoader.UpLoad(command.Picture, "slides");
        slide.Edit(pictureName, command.PictureAlt, command.PictureTitle,
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