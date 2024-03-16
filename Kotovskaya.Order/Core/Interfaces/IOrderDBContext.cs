using Microsoft.EntityFrameworkCore;

namespace Kotovskaya.Order.Core.Interfaces
{
    public interface IOrderDBContext
    {
        DbSet<OrderEntity> OrderEntities { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
