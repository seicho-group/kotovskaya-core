using Kotovskaya.DB.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kotovskaya.DB.Domain.Context;

public class KotovskayaDbContext : DbContext
{
    public DbSet<Category> Categories { get; set; }
    
    public DbSet<ProductEntity> Products { get; set; }
    
    public DbSet<SaleTypes> SaleTypes { get; set; }

    public KotovskayaDbContext()
    {
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Host=31.184.240.135;Database=postgres;Username=root;Password=123123");
}