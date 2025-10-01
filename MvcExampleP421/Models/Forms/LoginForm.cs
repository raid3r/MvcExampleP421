using System.ComponentModel.DataAnnotations;

namespace MvcExampleP421.Models.Forms;

public class LoginForm
{
    [Required]
    [EmailAddress]
    [Display(Name = "Електронна пошта")]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Пароль")]
    public string Password { get; set; }
}
