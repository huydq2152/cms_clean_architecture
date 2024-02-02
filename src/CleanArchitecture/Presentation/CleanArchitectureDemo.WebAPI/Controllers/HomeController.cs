using Microsoft.AspNetCore.Mvc;

namespace CleanArchitectureDemo.WebAPI.Controllers;

public class HomeController: ControllerBase
{
    public ActionResult Index()
    {
        return Redirect("~/swagger");
    }
}