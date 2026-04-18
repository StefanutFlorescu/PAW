using Lab06.Services;
using Lab06.ViewModels;
using Lab06.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Lab06.Controllers
{
    public class HomeController : Controller
    {
        private readonly IArticleService _articleService;
        private readonly ICategoryService _categoryService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IArticleService articleService, ICategoryService categoryService, ILogger<HomeController> logger)
        {
            _articleService = articleService;
            _categoryService = categoryService;
            _logger = logger;
        }

        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            const int recentCount = 3;
            var recentArticles = await _articleService.GetPagedAsync(1, recentCount, null, cancellationToken);
            var totalArticles = await _articleService.CountAsync(null, cancellationToken);
            var categories = await _categoryService.GetAllAsync(cancellationToken);
            var totalCategories = categories.Count;

            var viewModels = recentArticles.Select(a => new ArticleViewModel
            {
                Id = a.Id,
                Title = a.Title,
                Content = a.Content,
                PublishedAt = a.PublishedAt,
                CategoryName = a.Category?.Name ?? "N/A",
                AuthorName = a.Author?.FullName ?? "N/A",
                ImagePath = a.ImagePath
            }).ToList();

            ViewBag.TotalArticles = totalArticles;
            ViewBag.TotalCategories = totalCategories;

            return View(viewModels);
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
