using Kotovskaya.DB.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Kotovskaya.DB.Domain.Context;

public class KotovskayaDbContext : DbContext
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }

    public KotovskayaDbContext()
    {
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Host=31.184.240.135;Database=postgres;Username=root;Password=123123");
}