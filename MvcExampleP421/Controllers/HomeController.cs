using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using MvcExampleP421.Models;

namespace MvcExampleP421.Controllers;

public class HomeController(StoreContext context) : Controller
{
    public IActionResult Index()
    {
        ViewData["Categories"] = context.Categories.OrderBy(x => x.Name).ToList();
        var products = context
            .Products
            .Include(p => p.Category)
            .Include(p => p.ImageFile)
            .ToList();

        return View(products);
    }

    public IActionResult Category(int id)
    {
        ViewData["Categories"] = context.Categories.OrderBy(x => x.Name).ToList();

        var category = context.Categories
            .Include(c => c.Products)
            .ThenInclude(p => p.ImageFile)
            .FirstOrDefault(c => c.Id == id);

        return View(category);
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
