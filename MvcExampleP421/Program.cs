using Microsoft.EntityFrameworkCore;
using MvcExampleP421.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ImageFileStorageService>();


builder.Services.AddDbContext<StoreContext>(options =>
{
    options.UseSqlite("Data Source=store.db");
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// GET /Home/Index
// GET /Home/Privacy
// GET /Home/Error

//       contoller / action  / id?        
//            Home /  Index  /  ?
//            Home /  Privacy  /  ?
//            Home /  Error  /  ?

//  /           Home     Index        ?
//          /Category/ ?Index  / ?   

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
    );

app.Run();


/*
 * Зробити сторінку статистики з інформацією:
 * 
 * Кількість товарів
 * Кількість категорій
 * Список категорій з кількістю товарів в кожній, кількістю продажів в кожній категорії, загальною сумою продажів в кожній категорії
 * Загальна сума продажів 
 */


/*
 * Додати для продуктів зображення.
 * У формі створення/редагування продукту додати можливість завантаження зображення.
 * У списку продуктів та на сторінці редагування показувати зображення.
 * 
 * 
 */ 