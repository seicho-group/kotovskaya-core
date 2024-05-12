namespace Kotovskaya.DB.Domain.Entities;

public class OrderPosition
{
    public required Guid Id { get; init; }
    
    public Guid OrderId { get; init; }
    
    public required Order Order { get; init; }
    
    public int Quantity { get; set; }

    public string ProductId { get; set; }
    
    public required ProductEntity  Product { get; set; }
}