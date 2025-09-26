using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcExampleP421.Models;
using MvcExampleP421.Models.ViewModels;

namespace MvcExampleP421.Controllers;

public class OrderController(StoreContext context) : Controller
{
    public async Task<IActionResult> Index(int id)
    {
        var order = await context.Orders
            .Include(o => o.Items)
            .ThenInclude(oi => oi.Product)
            .ThenInclude(p => p.ImageFile)
            .FirstAsync(o => o.Id == id);

        return View(order);
    }
}
