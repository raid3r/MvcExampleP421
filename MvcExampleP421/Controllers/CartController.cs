using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcExampleP421.Models;
using MvcExampleP421.Models.ViewModels;
using System.Security.Claims;

namespace MvcExampleP421.Controllers;

public class CartController(StoreContext context, UserManager<User> userManager) : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Render([FromBody] List<CartItemRenderViewModel> cartItems)
    {
        cartItems.ForEach(item =>
        {
            item.Product = context.Products
            .Include(x => x.ImageFile)
            .FirstOrDefault(x => x.Id == item.Id);
        });

        return PartialView("_CartPartial", cartItems);
    }


    private async Task<User> GetCurrentUser()
    {
        var currentUserId = int.Parse(User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
        var user = await userManager.Users
            .Include(x => x.ImageFile)
            .FirstAsync(x => x.Id == currentUserId);
        return user;
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> MakeOrder([FromBody] List<CartItemViewModel> cartItems)
    {
        // Отримати поточного користувача
        var user = await GetCurrentUser();

        // Створити список позицій замовлення на основі елементів корзини
        var orderItems = new List<OrderItem>();

        // Заповнити список позицій замовлення
        foreach (var cartItem in cartItems)
        {
            // Знайти продукт у базі даних за ідентифікатором
            var product = context.Products.First(x => x.Id == cartItem.Id);

            // Створити позицію замовлення
            var orderItem = new OrderItem
            {
                Product = product,
                Quantity = cartItem.Quantity,
                Price = product.Price
            };
            // Додати позицію до списку
            orderItems.Add(orderItem);
        }

        // Створити замовлення
        var order = new Order
        {
            CreatedAt = DateTime.Now,
            Items = orderItems,
            User = user
        };


        foreach (var item in orderItems)
        {
            // Створити запис про продаж
            var sale = new Sale
            {
                Product = item.Product,
                Quantity = item.Quantity,
                Price = item.Price,
                SaleDate = DateTime.UtcNow
            };
            // Додати запис про продаж до контексту
            context.Sales.Add(sale);
        }

        // Додати замовлення до контексту
        context.Orders.Add(order);

        // Зберегти зміни у базі даних
        context.SaveChanges();

        return Json(new { id = order.Id });
    }


}
