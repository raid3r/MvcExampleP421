using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MvcExampleP421.Models;

namespace MvcExampleP421.Controllers;

public class HomeController : Controller
{
    // GET /Home/Index
    // GET /
    public IActionResult Index()
    {
        return View();
    }

    // GET /Home/Privacy
    public IActionResult Privacy()
    {
        return View();
    }

    // GET /Home/Error
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
