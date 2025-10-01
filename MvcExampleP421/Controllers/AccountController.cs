using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MvcExampleP421.Models;
using MvcExampleP421.Models.Forms;
using System.Security.Claims;

namespace MvcExampleP421.Controllers;

public class AccountController(UserManager<User> userManager) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Login(string? returnUrl)
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromForm] LoginForm form, string? returnUrl)
    {
        // Перевірка валідності моделі (заповнені дані)
        if (!ModelState.IsValid)
        {
            return View(form);
        }

        var user = await userManager.FindByEmailAsync(form.Email);
        if (user == null)
        {
            ModelState.AddModelError(string.Empty, "Неправильна логін");
            return View(form);
        }

        if (!await userManager.CheckPasswordAsync(user, form.Password))
        {
            ModelState.AddModelError(string.Empty, "Неправильний пароль");
            return View(form);
        }

        var identity = new ClaimsIdentity(IdentityConstants.ApplicationScheme);

        identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
        identity.AddClaim(new Claim(ClaimTypes.Email, user.Email));

        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync(IdentityConstants.ApplicationScheme, principal);


        if (!string.IsNullOrEmpty(returnUrl))
        {
            return Redirect(returnUrl);
        }

        return RedirectToAction("Index", "Home");
    }


    [HttpGet]
    public async Task<IActionResult> Register(string? returnUrl)
    {
        return View(new RegisterForm());
    }

    [HttpPost]
    public async Task<IActionResult> Register([FromForm] RegisterForm form, string? returnUrl)
    {
        // Перевірка валідності моделі (заповнені дані)
        if (!ModelState.IsValid)
        {
            return View(form);
        }

        // Перевірка відповідності паролів (співпадіння паролю та підтвердження)
        if (form.Password != form.ConfirmPassword)
        {
            ModelState.AddModelError(nameof(form.ConfirmPassword), "Паролі не співпадають.");
            return View(form);
        }

        // Перевірка унікальності електронної пошти (чи вже існує користувач з такою електронною поштою)
        if (await userManager.FindByEmailAsync(form.Email) != null)
        {
            ModelState.AddModelError(nameof(form.Email), "Користувач з такою електронною поштою вже існує.");
            return View(form);
        }

        // Створення нового користувача
        var user = new User
        {
            UserName = form.Email,
            Email = form.Email,
        };

        var result = await userManager.CreateAsync(user, form.Password);

        if (!result.Succeeded)
        {
            // result.Errors містить інформацію про помилки, які виникли під час створення користувача
            ModelState.AddModelError(nameof(form.Equals), "Помилка при створенні користувача.");
            return View(form);
        }

        var identity = new ClaimsIdentity(IdentityConstants.ApplicationScheme);

        identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
        identity.AddClaim(new Claim(ClaimTypes.Email, user.Email));
        
        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync(IdentityConstants.ApplicationScheme, principal);


        if (!string.IsNullOrEmpty(returnUrl))
        {
            return Redirect(returnUrl);
        }

        return RedirectToAction("Index", "Home");
    }


    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
        return RedirectToAction("Index", "Home");
    }
}
