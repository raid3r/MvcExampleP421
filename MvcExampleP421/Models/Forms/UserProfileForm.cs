using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcExampleP421.Models.Forms;

public class UserProfileForm
{
    [DisplayName("Телефон")]
    public string? Phone { get; set; }
    [DisplayName("Аватарка")]
    public IFormFile? ImageFile { get; set; }
}
