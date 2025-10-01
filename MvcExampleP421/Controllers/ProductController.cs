using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcExampleP421.Models;
using MvcExampleP421.Models.Forms;

namespace MvcExampleP421.Controllers;

[Authorize]
public class ProductController(
    StoreContext context,
    ImageFileStorageService storageService
    ) : Controller
{
    public IActionResult Index()
    {
        ViewData["Categories"] = context.Categories.ToList();
        return View(context.Products.Include(x => x.ImageFile).ToList());
    }

    [HttpGet]
    public IActionResult Create()
    {
        ViewData["Categories"] = context.Categories.ToList();
        return View(new ProductForm());
    }

    [HttpPost]

    /*
     * [FromForm] - вказує, що дані для параметра form повинні бути отримані з форми HTTP-запиту.
     * [FromBody] - вказує, що дані для параметра повинні бути отримані з тіла HTTP-запиту.
     * [FromQuery] - вказує, що дані для параметра повинні бути отримані з рядка запиту URL.
     * [FromHeader] - вказує, що дані для параметра повинні бути отримані з заголовків HTTP-запиту.
     * [FromRoute] - вказує, що дані для параметра повинні бути отримані з маршрутних даних (наприклад, з URL-шаблону маршруту).
     * [FromServices] - вказує, що параметр повинен бути отриманий через механізм впровадження залежностей (Dependency Injection).
     */

    public async Task<IActionResult> Create([FromForm] ProductForm form)
    {
        if (ModelState.IsValid)
        {
            // Створення нового продукту
            var product = new Product
            {
                Name = form.Name,
                Description = form.Description,
                Price = form.Price,
                CategoryId = form.CategoryId
            };
            // Обробка файла зображення
            if (form.ImageFile != null)
            {
                // Збереження файла зображення і отримання унікального імені файлу
                var image = new ImageFile
                {
                    FileName = await storageService.StoreFileAsync(form.ImageFile),
                };
                product.ImageFile = image;
            }

            await context.Products.AddAsync(product);
            await context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        ViewData["Categories"] = context.Categories.ToList();
        return View(form);
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var product = context.Products.FirstOrDefault(p => p.Id == id);
        if (product == null)
        {
            return NotFound();
        }
        ViewData["Categories"] = context.Categories.ToList();

        var form = new ProductForm
        {
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            CategoryId = product.CategoryId,
        };

        return View(form);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, [FromForm] ProductForm form)
    {
        if (ModelState.IsValid)
        {
            var product = await context.Products
                .Include(x => x.ImageFile)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }
            product.Name = form.Name;
            product.Description = form.Description;
            product.Price = form.Price;
            product.CategoryId = form.CategoryId;

            if (form.ImageFile != null)
            {
                // Якщо у продукту вже є файл зображення, видаляємо його
                if (product.ImageFile != null)
                {
                    // Видалення старого файла зображення
                    storageService.DeleteFile(product.ImageFile.FileName);
                    // Видалення запису про файл з бази даних
                    context.ImageFiles.Remove(product.ImageFile);
                }

                // Збереження файла зображення і отримання унікального імені файлу
                var image = new ImageFile
                {
                    FileName = await storageService.StoreFileAsync(form.ImageFile),
                };
                product.ImageFile = image;
            }


            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        ViewData["Categories"] = context.Categories.ToList();
        return View(form);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var product = await context.Products
            .Include(x => x.ImageFile)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (product != null)
        {
            // Якщо у продукту є файл зображення, видаляємо його
            if (product.ImageFile != null)
            {
                // Видалення файла зображення
                storageService.DeleteFile(product.ImageFile.FileName);
                // Видалення запису про файл з бази даних
                context.ImageFiles.Remove(product.ImageFile);
            }

            context.Products.Remove(product);
            await context.SaveChangesAsync();
        }
        return RedirectToAction("Index");
    }
}
