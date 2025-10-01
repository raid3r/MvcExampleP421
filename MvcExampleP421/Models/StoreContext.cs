using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;

namespace MvcExampleP421.Models;

public class StoreContext : IdentityDbContext<User, IdentityRole<int>, int>
{
    public StoreContext()
    {
    }
    public StoreContext(DbContextOptions<StoreContext> options) : base(options)
    {
    }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Sale> Sales { get; set; }
    public DbSet<ImageFile> ImageFiles { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
}
