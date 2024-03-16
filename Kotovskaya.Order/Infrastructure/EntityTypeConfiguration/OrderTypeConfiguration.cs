using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Kotovskaya.Order.Core.Interfaces;

namespace Kotovskaya.Order.Infrastrucure.EntityTypeConfiguration
{
    public class OrderTypeConfiguration : IEntityTypeConfiguration<OrderEntity>
    {
        public void Configure(EntityTypeBuilder<OrderEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Id).IsUnique();
            builder.Property(x => x.Orderer).IsRequired();
            builder.Property(x => x.OrderNumber).IsRequired();
            builder.Property(x => x.TotalPrice).IsRequired();
        }
    }
}
