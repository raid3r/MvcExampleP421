namespace MvcExampleP421.Models.ViewModels;

public class DashboardDataViewModel
{
    //    * Кількість товарів
    public int TotalProducts { get; set; }
    //* Кількість категорій
    public int TotalCategories { get; set; }
    //* Загальна сума продажів
    public decimal TotalSales { get; set; }

    //* Список категорій з кількістю товарів в кожній,
    //  кількістю продажів в кожній категорії,
    //  загальною сумою продажів в кожній категорії
    //   Name, ProductCount, SaleCount, TotalSalesAmount
    public List<CategorySalesData> CategorySales { get; set; } = [];
}
