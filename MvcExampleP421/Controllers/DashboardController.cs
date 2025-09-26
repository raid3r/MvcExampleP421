using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcExampleP421.Models;
using MvcExampleP421.Models.ViewModels;

namespace MvcExampleP421.Controllers;

public class DashboardController(StoreContext context) : Controller
{
    public IActionResult Index()
    {
        /*
         * Зробити сторінку статистики з інформацією:
         * 
         * Кількість товарів
         * Кількість категорій
         * Список категорій з кількістю товарів в кожній, кількістю продажів в кожній категорії, загальною сумою продажів в кожній категорії
         * Загальна сума продажів 
         * 
         */

        var model = new DashboardDataViewModel
        {
            TotalProducts = context.Products.Count(),
            TotalCategories = context.Categories.Count(),
            TotalSales = context.Sales.AsEnumerable().Sum(s => s.Price * s.Quantity),
            CategorySales = context.Sales
            .Include(s => s.Product)
            .ThenInclude(p => p.Category)
            .AsEnumerable()
            .GroupBy(s => s.Product.Category)
            .Select(g => new CategorySalesData
            {
                Name = g.Key.Name,
                SaleCount = g.Count(),
                TotalSalesAmount = g.Sum(s => s.Price * s.Quantity),
            }).ToList()
        };



        return View(model);
    }
}




