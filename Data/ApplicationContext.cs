using Microsoft.EntityFrameworkCore;
using Product_Manager.Domain;
using System.Diagnostics;

namespace Product_Manager.Data;

public class ApplicationContext : DbContext
{
    //private readonly string connectionString;

    //public ApplicationContext(string connectionString)
    //{
    //    this.connectionString = connectionString;
    //}

    static string connectionString = "Server=.;Database=ProductManager;Integrated Security=True;Encrypt=False";

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(connectionString);
    }

    public DbSet<Product> Product { get; set; }
    public DbSet<Category> Category { get; set; }
    public DbSet<ProductCategory> ProductCategory { get; set; }
}
