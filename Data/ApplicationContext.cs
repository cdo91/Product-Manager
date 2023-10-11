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

    //En anslutningssträng för att ansluta applikationen till en SQL Server-databas med de angivna inställningarna.
    static string connectionString = "Server=.;Database=ProductManager;Integrated Security=True;Encrypt=False";

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) // Konfigurerar anslutningsalternativ för Entity Framework Core.
    {
        optionsBuilder.UseSqlServer(connectionString); // Anger att vi använder en SQL Server-databas med den angivna anslutningssträngen.
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Category>()
            .HasMany(c => c.Product)
            .WithMany(p => p.Category)
            .UsingEntity(j => j.ToTable("CategoryProduct"));
    }

    // DbSet-egenskaper för att ange vilka klasser som ska inkluderas i databasen.
    public DbSet<Product> Product { get; set; }
    public DbSet<Category> Category { get; set; }



}
