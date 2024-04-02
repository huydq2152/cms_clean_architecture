using CleanArchitecture.Application.Dtos.Posts.PostCategory;
using CleanArchitecture.Application.Interfaces.Services.Posts;
using CleanArchitecture.WebApp.Models;
using CleanArchitecture.WebApp.Models.Common;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.WebApp.Components;

public class NavigationViewComponent : ViewComponent
{
    private readonly IPostCategoryService _postCategoryService;

    public NavigationViewComponent(IPostCategoryService postCategoryService)
    {
        _postCategoryService = postCategoryService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var model = await _postCategoryService.GetAllPostCategoriesAsync(new GetAllPostCategoriesInput());
        var navItems = model.Select(x => new NavigationItemViewModel()
        {
            Slug = x.Slug,
            Name = x.Name,
            Children = model.Where(x => x.ParentId == x.Id).Select(i => new NavigationItemViewModel()
            {
                Name = x.Name,
                Slug = x.Slug
            }).ToList()
        }).ToList();
        return View(navItems);
    }
}