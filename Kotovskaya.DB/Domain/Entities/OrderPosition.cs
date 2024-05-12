using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kotovskaya.DB.Domain.Entities;

public class OrderPosition
{
    [Key]
    public Guid Id { get; init; }
    
    [ForeignKey("OrderId")]
    public Order OrderId { get; init; }
    
    public int Price { get; init; }
    
    public int? SalePrice { get; init; }

}