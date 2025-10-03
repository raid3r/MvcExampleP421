using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcExampleP421.Models;

public class User: IdentityUser<int>
{
    public int? ImageFileId { get; set; }
    [ForeignKey("ImageFileId")]
    public virtual ImageFile? ImageFile { get; set; }
}
