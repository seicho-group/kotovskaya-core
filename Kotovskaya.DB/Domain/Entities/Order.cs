namespace Kotovskaya.DB.Domain.Entities;

public class Order
{
    public Guid Id { get; init; }
    
    public required string MoySkladNumber { get; set; }

    public List<OrderPosition> OrderPositions { get; set; } = [];

    public required string AuthorName { get; set; }
    
    public required string AuthorPhone { get; set; }
    
    public required string AuthorEmail { get; set; }

    public bool HasAuthorDiscount { get; set; } = false;

    public string? Comment { get; set; }

    public DeliveryWay DeliveryWay { get; set; } = DeliveryWay.Self;

    public string? DeliveryAddress { get; set; }

    public DateTime OrderDateTime { get; set; }

    public bool IsTestOrder { get; set; } = false;

    public OrderStatus Status { get; set; } = OrderStatus.Open;

    public string? PriorityOrderDate { get; set; }
}