using System.ComponentModel.DataAnnotations;

namespace MvcExampleP421.Models.Forms;

public class RegisterForm
{
    [Required]
    [EmailAddress]
    [Display(Name = "Електронна пошта")]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Пароль")]
    public string Password { get; set; }

    [Required]
    [Display(Name = "Підтвердженння паролю")]
    [Compare("Password", ErrorMessage = "Паролі не співпадають")]
    [DataType(DataType.Password)]
    public string ConfirmPassword { get; set; }
}
