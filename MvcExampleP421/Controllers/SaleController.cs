using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcExampleP421.Models;

namespace MvcExampleP421.Controllers;

[Authorize]
public class SaleController(StoreContext context) : Controller
{
    public IActionResult List(int id)
    {
        var product = context.Products
            .Include(x => x.ImageFile)
            .Include(p => p.Category)
            .Include(p => p.Sales)
            .First(p => p.Id == id);

        return View(product);
    }

    // GET: Sale/Create/5
    [HttpGet]
    public IActionResult Create(int id)
    {
        return View(new Sale
        {
            Product = context.Products.Include(x => x.ImageFile).First(x => x.Id == id),
        });
    }

    [HttpPost]
    public IActionResult Create(int id, [FromForm]Sale sale)
    {
        ModelState.Remove(nameof(Sale.Price));
        ModelState.Remove(nameof(Sale.SaleDate));
        ModelState.Remove(nameof(Sale.ProductId));

        var product = context.Products.Include(x => x.ImageFile).First(x => x.Id == id);

        if (ModelState.IsValid)
        {
            sale.SaleDate = DateTime.Now;
            sale.Product = product;
            sale.Price = product.Price;

            context.Sales.Add(sale);
            context.SaveChanges();
            return RedirectToAction("Index", new {Controller = "Product"});
        }

        sale.Product = product;
        return View(sale);
    }
}