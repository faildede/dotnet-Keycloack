using Microsoft.EntityFrameworkCore;

namespace eshop_auth.Models;

public class ApplicationContext: DbContext
{
    public DbSet<ProductsModels> Products => Set<ProductsModels>();
    public ApplicationContext() => Database.EnsureCreated();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(@"Host=localhost;Username=admin;Password=root;Database=db");
    }
}