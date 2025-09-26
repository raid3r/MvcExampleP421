namespace MvcExampleP421.Models.ViewModels;

public class CartItemRenderViewModel: CartItemViewModel
{
    public Product? Product { get; set; } = null!;

    public decimal TotalPrice => Product != null ? Product.Price * Quantity : 0;
}
