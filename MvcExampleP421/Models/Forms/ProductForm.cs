using System.ComponentModel.DataAnnotations.Schema;

namespace MvcExampleP421.Models.Forms;

public class ProductForm
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
    public IFormFile? ImageFile { get; set; }
}
