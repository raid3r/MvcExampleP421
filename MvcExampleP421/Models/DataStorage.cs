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

    }

    public List<Category> Categories { get; set; } = [];
}
