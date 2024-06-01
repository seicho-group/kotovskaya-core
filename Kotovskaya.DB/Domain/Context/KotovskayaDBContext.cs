using DotNetEnv;
using Kotovskaya.DB.Domain.Entities.DatabaseEntities;
using Microsoft.EntityFrameworkCore;

namespace Kotovskaya.DB.Domain.Context;

public class KotovskayaDbContext : DbContext
{
    public KotovskayaDbContext()
    {
        Env.TraversePath().Load();
    }

    public DbSet<Category> Categories { get; set; }

    public DbSet<ProductEntity> Products { get; set; }

    public DbSet<SaleTypes> SaleTypes { get; set; }

    public DbSet<Order> Orders { get; set; }

    public DbSet<OrderPosition> OrderPositions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        optionsBuilder.UseNpgsql($"" +
                                 $"Host={Environment.GetEnvironmentVariable("PG_HOST")};" +
                                 $"Database={Environment.GetEnvironmentVariable(env == "development" ? "PG_DB_STAGE" : "PG_DB")};" +
                                 $"Username=root;" +
                                 $"Password={Environment.GetEnvironmentVariable("PG_PASS")}");
    }
}
