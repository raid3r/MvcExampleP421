using Microsoft.AspNetCore.Mvc;
using MvcExampleP421.Models;

namespace MvcExampleP421.Controllers;

public class CategoryController(DataStorage dataStorage) : Controller
{
    // GET /Category/Index
    public IActionResult Index()
    {
        var categories = dataStorage.Categories;

        var message = "Моє повідомлення";

        var date = DateTime.Now.AddDays(30);

        // Запам'ятовуємо дані у ViewData
        ViewData["Categories"] = categories;
        ViewData["Message"] = message;
        ViewData["Date"] = date;

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
            category.Id = dataStorage.Categories.Count > 0 ? dataStorage.Categories.Max(c => c.Id) + 1 : 1;
            dataStorage.Categories.Add(category);
            // Логіка збереження категорії (наприклад, у базу даних)
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
        var category = dataStorage.Categories.FirstOrDefault(c => c.Id == id);
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
            var category = dataStorage.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            category.Name = updatedCategory.Name;
            
            // Логіка оновлення категорії (наприклад, у базу даних)
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
        var category = dataStorage.Categories.FirstOrDefault(c => c.Id == id);
        if (category == null)
        {
            return NotFound();
        }
        dataStorage.Categories.Remove(category);
        // Логіка видалення категорії (наприклад, з бази даних)
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