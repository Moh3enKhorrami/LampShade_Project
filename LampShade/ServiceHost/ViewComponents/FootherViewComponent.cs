using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents;

public class FootherViewComponent : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();
    }
}