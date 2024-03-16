using Kotovskaya.Order.Core.Interfaces;
using Kotovskaya.Order.Application.DB.EntityTypeConfiguration;
using Microsoft.EntityFrameworkCore;

namespace Kotovskaya.Order.Application.DB
{
    public class OrderDBContext : DbContext, IOrderDBContext
    {
        public DbSet<OrderEntity> OrderEntities { get; set; }

        public OrderDBContext(DbContextOptions<OrderDBContext> options) : base(options)
        { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OrderTypeConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
