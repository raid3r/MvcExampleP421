using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MvcExampleP421.Models;

namespace MvcExampleP421.Controllers;

[Authorize]
public class CategoryController(StoreContext context) : Controller
{
    // GET /Category/Index
    public IActionResult Index()
    {
        var categories = context.Categories.ToList();
        ViewData["Categories"] = categories;
        return View();
    }

    // GET /Category/Create
    [HttpGet]
    public IActionResult Create()
    {
        return View(new Category());
    }

    // POST /Category/Create
    [HttpPost]
    public IActionResult Create([FromForm]Category category)
    {
        if (ModelState.IsValid)
        {
            // Логіка збереження категорії (наприклад, у базу даних)
            context.Categories.Add(category);
            context.SaveChanges();
            // Після збереження перенаправляємо на сторінку зі списком категорій
            return RedirectToAction("Index");
        }
        // Якщо модель недійсна, повертаємося до форми створення з помилками
        return View(category);
    }

    // GET /Category/Edit/3
    [HttpGet]
    public IActionResult Edit(int id)
    {
        var category = context.Categories.FirstOrDefault(c => c.Id == id);
        if (category == null)
        {
            return NotFound();
        }
        return View(category);
    }

    // POST /Category/Edit/3
    [HttpPost]
    public IActionResult Edit(int id, [FromForm]Category updatedCategory)
    {
        if (ModelState.IsValid)
        {
            var category = context.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            category.Name = updatedCategory.Name;
            
            // Логіка оновлення категорії (наприклад, у базу даних)
            context.SaveChanges();
            // Після оновлення перенаправляємо на сторінку зі списком категорій
            return RedirectToAction("Index");
        }
        // Якщо модель недійсна, повертаємося до форми редагування з помилками
        return View(updatedCategory);
    }

    [HttpPost]
    // POST /Category/Delete/3
    public IActionResult Delete(int id)
    {
        var category = context.Categories.FirstOrDefault(c => c.Id == id);
        if (category == null)
        {
            return NotFound();
        }
        // Логіка видалення категорії (наприклад, з бази даних)
        context.Categories.Remove(category);
        context.SaveChanges();
        // Після видалення перенаправляємо на сторінку зі списком категорій
        return RedirectToAction("Index");
    }
}


/*
 * Створити контроллер для роботи з продуктами
 * Перегляд списку продуктів
 * 
 * Під час створення та редагування продукту можна вибрати категорію з випадаючого списку
 * Список категорій редагується через CategoryController
 * 
 * Створення продукту
 * Редагування продукту
 * Видалення продукту
 * 
 * Продукт:
 * - Id
 * - Назва
 * - Опис
 * - Ціна
 * - Категорія (вибір зі списку)
 * 
 */ 