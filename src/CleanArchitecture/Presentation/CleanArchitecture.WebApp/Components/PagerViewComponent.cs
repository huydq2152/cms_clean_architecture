using Microsoft.AspNetCore.Mvc;
using Shared.SeedWork.Paging;

namespace CleanArchitecture.WebApp.Components;

public class PagerViewComponent: ViewComponent
{
    public Task<IViewComponentResult> InvokeAsync(PagedResultMetaData result)
    {
        return Task.FromResult((IViewComponentResult)View("Default", result));
    }
}