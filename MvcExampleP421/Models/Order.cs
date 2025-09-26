namespace MvcExampleP421.Models;

public class Order
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public virtual ICollection<OrderItem> Items { get; set; } = [];
}
