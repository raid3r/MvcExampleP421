using System.ComponentModel.DataAnnotations;

namespace MvcExampleP421.Models.Forms;

public class ChangePasswordForm
{
    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Старий пароль")]
    public string OldPassword { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Новий пароль")]
    public string NewPassword { get; set; }

    [Required]
    [Display(Name = "Підтвердженння нового паролю")]
    [Compare("NewPassword", ErrorMessage = "Паролі не співпадають")]
    [DataType(DataType.Password)]
    public string ConfirmNewPassword { get; set; }
}
