using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.WebAPI.Controllers.Common;

public class HomeController: ControllerBase
{
    public ActionResult Index()
    {
        return Redirect("~/swagger");
    }
}