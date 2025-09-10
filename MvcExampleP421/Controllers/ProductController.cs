using Microsoft.AspNetCore.Mvc;

namespace MvcExampleP421.Controllers;

public class ProductController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
