using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcExampleP421.Models;

public class Product 
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }

    public int CategoryId { get; set; }
    [ForeignKey("CategoryId")]
    public virtual Category Category { get; set; }

    public int? ImageFileId { get; set; }
    [ForeignKey("ImageFileId")]
    public virtual ImageFile? ImageFile { get; set; }

    public virtual List<Sale> Sales { get; set; } = [];

}
