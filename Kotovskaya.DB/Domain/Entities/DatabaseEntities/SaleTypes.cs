using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kotovskaya.DB.Domain.Entities.DatabaseEntities;

public class SaleTypes
{
    [Key] public Guid Id { get; init; }

    [ForeignKey("ProductId")] public required ProductEntity Product { get; init; }

    public int Price { get; set; }

    public int? OldPrice { get; set; }
}