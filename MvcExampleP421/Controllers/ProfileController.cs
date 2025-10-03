using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcExampleP421.Models;
using MvcExampleP421.Models.Forms;
using System.Security.Claims;

namespace MvcExampleP421.Controllers;

[Authorize]
public class ProfileController(
    UserManager<User> userManager, 
    ImageFileStorageService storageService,
    StoreContext context
    ) : Controller
{
    public async Task<IActionResult> Index()
    {
        return View(await GetCurrentUser());
    }

    private async Task<User> GetCurrentUser()
    {
        var currentUserId = int.Parse(User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
        var user = await userManager.Users
            .Include(x => x.ImageFile)
            .FirstAsync(x => x.Id == currentUserId);
        return user;
    }

    [HttpGet]
    public async Task<IActionResult> Edit()
    {
        var user = await GetCurrentUser();
        ViewData["User"] = user;

        var form = new UserProfileForm
        {
            Phone = user.PhoneNumber,
        };

        return View(form);
    }


    [HttpPost]
    public async Task<IActionResult> Edit([FromForm] UserProfileForm form)
    {
        var user = await GetCurrentUser();

        if (!ModelState.IsValid)
        {
            ViewData["User"] = user;
            return View(form);
        }
        
        user.PhoneNumber = form.Phone;

        if (form.ImageFile != null)
        {
            if (user.ImageFile != null)
            {
                storageService.DeleteFile(user.ImageFile.FileName);
                context.ImageFiles.Remove(user.ImageFile);
            }

            var image = new ImageFile
            {
                FileName = await storageService.StoreFileAsync(form.ImageFile),
            };
            user.ImageFile = image;
        }
        await context.SaveChangesAsync();
        await userManager.UpdateAsync(user);


        return RedirectToAction(nameof(Index));
    }

}
