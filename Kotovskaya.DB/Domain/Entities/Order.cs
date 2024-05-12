using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kotovskaya.DB.Domain.Entities;

public class Order
{
    [Key]
    public Guid Id { get; init; }
    
    
    public int Price { get; init; }
    
    public int? SalePrice { get; init; }

}