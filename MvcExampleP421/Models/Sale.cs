namespace MvcExampleP421.Models;

public class Sale
{
    public int Id { get; set; }

    public int ProductId { get; set; }
    public virtual Product? Product { get; set; }

    public decimal Price { get; set; }
    public int Quantity { get; set; }

    public DateTime SaleDate { get; set; }
}
