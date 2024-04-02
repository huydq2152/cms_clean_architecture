using CleanArchitecture.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using CleanArchitecture.Application.Interfaces.Services.Posts;
using CleanArchitecture.WebApp.Models.Common;

namespace CleanArchitecture.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPostService _postService;

        public HomeController(IPostService postService)
        {
            _postService = postService;
        }

        public async Task<IActionResult> Index()
        {
            var model = new HomeViewModel()
            {
                LatestPosts = await _postService.GetLatestPublishedPostsAsync(10)
            };
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}