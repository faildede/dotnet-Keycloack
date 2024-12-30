using Microsoft.EntityFrameworkCore;

namespace eshop_auth.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

          public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           optionsBuilder.UseNpgsql(@"Host=localhost;Username=admin;Password=root;Database=db");    
        }
    }
}
