using Contracts.Common.Models.Paging;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.WebApp.Components;

public class PagerViewComponent: ViewComponent
{
    public Task<IViewComponentResult> InvokeAsync(PagedResultMetaData result)
    {
        return Task.FromResult((IViewComponentResult)View("Default", result));
    }
}