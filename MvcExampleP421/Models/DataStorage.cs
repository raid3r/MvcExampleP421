namespace MvcExampleP421.Models;

public class DataStorage
{
    public DataStorage()
    {
        Categories.AddRange(new List<Category>
            {
                new Category { Id = 1, Name = "Electronics" },
                new Category { Id = 2, Name = "Books" },
                new Category { Id = 3, Name = "Clothing" }
            });

        Products.AddRange(new List<Product>
            {
                new Product { Id = 1, Name = "Laptop", Description = "A high-performance laptop.", Price = 999.99m, CategoryId = 1 },
                new Product { Id = 2, Name = "Smartphone", Description = "A latest model smartphone.", Price = 699.99m, CategoryId = 1 },
                new Product { Id = 3, Name = "Novel", Description = "A bestselling novel.", Price = 19.99m, CategoryId = 2 },
                new Product { Id = 4, Name = "T-Shirt", Description = "A comfortable cotton t-shirt.", Price = 9.99m, CategoryId = 3 }
            });
    }

    public List<Category> Categories { get; set; } = [];
    public List<Product> Products { get; set; } = [];   
}
