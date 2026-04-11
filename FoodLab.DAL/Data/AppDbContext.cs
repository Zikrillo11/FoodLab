using FoodLab.Domain.Entites;
using Microsoft.EntityFrameworkCore;

namespace FoodLab.DAL.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItem { get; set; }
    public DbSet<Product> Products { get; set; }
}
