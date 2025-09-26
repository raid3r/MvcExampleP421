﻿namespace MvcExampleP421.Models;

public class OrderItem
{
    public int Id { get; set; }
    public virtual Order? Order { get; set; }
    public virtual Product? Product { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}
