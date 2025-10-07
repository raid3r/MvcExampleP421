using Microsoft.AspNetCore.Identity;
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

builder.Services.AddIdentity<User, IdentityRole<int>>(options =>
{
    options.SignIn.RequireConfirmedPhoneNumber = false;
    options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedAccount = false;

    options.Password.RequiredLength = 3; // �������� ������� 3 �������
    options.Password.RequiredUniqueChars = 0; // ������� ��������� �������

    options.Password.RequireDigit = false; // �� ����'������ ���� �����
    options.Password.RequireLowercase = false; // �� ����'������ ���� ���� �����
    options.Password.RequireNonAlphanumeric = false; // �� ����'������ ���� ����������� ������
    options.Password.RequireUppercase = false; // �� ����'������ ���� ������ �����
    options.Password.RequireLowercase = false; // �� ����'������ ���� ���� �����

})
    .AddRoles<IdentityRole<int>>()
    .AddEntityFrameworkStores<StoreContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = new PathString("/Account/Register");
    options.LogoutPath = new PathString("/Account/Logout");
    options.AccessDeniedPath = new PathString("/Account/AccessDenied");
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

// ��������������
app.UseAuthentication();
// �����������
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


// �������� �� ��������� ���� ������������ �� ������������ �� �� �������������

using (var scope = app.Services.CreateScope())
{
    // ��������� ������
    var services = scope.ServiceProvider;

    // ̳������ ���� ����� ����������� ��� �������
    var context = services.GetRequiredService<StoreContext>();
    context.Database.Migrate();


    
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole<int>>>();
    var userManager = services.GetRequiredService<UserManager<User>>();

    string[] roles = ["Admin", "User"];

    // �������� �� ��������� ����� �� ��������� ��, ���� �� ����
    foreach (string role in roles)
    {
        var roleExist = await roleManager.RoleExistsAsync(role);
        if (!roleExist)
        {
            await roleManager.CreateAsync(new IdentityRole<int>(role));
        }
    }

    // ��������� ������������ �� ������������
    string adminEmail = "admin@test.com";
    string adminPassword = "admin";

    User adminUser = await userManager.FindByEmailAsync(adminEmail);
    if (adminUser == null)
    {
        adminUser = new User
        {
            UserName = adminEmail,
            Email = adminEmail,
        };
        var result = await userManager.CreateAsync(adminUser, adminPassword);
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }
    }
}

app.Run();


/*
 * ������� ������� ���������� � �����������:
 * 
 * ʳ������ ������
 * ʳ������ ��������
 * ������ �������� � ������� ������ � �����, ������� ������� � ����� �������, ��������� ����� ������� � ����� �������
 * �������� ���� ������� 
 */


/*
 * ������ ��� �������� ����������.
 * � ���� ���������/����������� �������� ������ ��������� ������������ ����������.
 * � ������ �������� �� �� ������� ����������� ���������� ����������.
 * 
 * 
 */


/*
 * ������� ������.
 * - ó���
 * - ������������� ���������� (��������)
 * - �����������
 * 
 */ 