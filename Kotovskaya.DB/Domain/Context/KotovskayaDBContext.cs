using Kotovskaya.DB.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kotovskaya.DB.Domain.Context;

public class KotovskayaDbContext : DbContext
{
    public DbSet<Category> Categories { get; set; }
    
    public DbSet<ProductEntity> Products { get; set; }
    
    public DbSet<SaleTypes> SaleTypes { get; set; }
    
    public DbSet<Order> Orders { get; set; }
    
    public DbSet<OrderPosition> OrderPositions { get; set; }

    public KotovskayaDbContext()
    {
        DotNetEnv.Env.TraversePath().Load();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql($"Host={Environment.GetEnvironmentVariable("PG_HOST")};Database={Environment.GetEnvironmentVariable("PG_DB")};Username=root;Password={Environment.GetEnvironmentVariable("PG_PASS")}");
}