using System.ComponentModel.DataAnnotations;

namespace Kotovskaya.DB.Domain.Entities.DatabaseEntities;

public class OrderPosition
{
    public Guid Id { get; init; }

    public required Guid OrderId { get; init; }

    public required Order Order { get; init; }

    public int Quantity { get; set; }

    [StringLength(150, MinimumLength = 5)] public required string ProductId { get; set; }

    public required ProductEntity Product { get; set; }
}