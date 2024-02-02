using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.WebAPI.Controllers;

public class HomeController: ControllerBase
{
    public ActionResult Index()
    {
        return Redirect("~/swagger");
    }
}