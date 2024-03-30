using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.WebApp.Components;

public class NavigationViewComponent: ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        return View();
    }
}