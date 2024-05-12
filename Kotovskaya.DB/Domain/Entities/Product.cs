using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kotovskaya.DB.Domain.Entities;

public class ProductEntity
{
    [StringLength(150, MinimumLength = 5)] 
    public string Id { get; init; } = Guid.NewGuid().ToString();
    
    [StringLength(150, MinimumLength = 5)] 
    public string Name { get; init; } = "Без имени";
    
    public string? Description { get; init; } = "";
    public Guid? MsId { get; init; }
    
    [StringLength(64, MinimumLength = 10)] 
    public string? Article { get; init; }
    public int Quantity { get; init; }
    
    public string? CategoryId { get; init; }
    public Category Category { get; init; } = null!;
    
    [StringLength(512, MinimumLength = 0)] 
    public string? ImageLink { get; set; }
    
    public SaleTypes? SaleTypes { get; set; }
}