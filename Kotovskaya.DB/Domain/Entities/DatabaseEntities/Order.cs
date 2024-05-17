using System.ComponentModel.DataAnnotations;
using Kotovskaya.DB.Domain.Entities.Enum;

namespace Kotovskaya.DB.Domain.Entities.DatabaseEntities;

public class Order
{
    public Guid Id { get; init; }
    
    [StringLength(150, MinimumLength = 5)] 
    public required string MoySkladNumber { get; init; }

    public ICollection<OrderPosition> OrderPositions { get; init; } = new List<OrderPosition>();

    [StringLength(64, MinimumLength = 5)]
    public required string AuthorName { get; set; }
    
    [StringLength(10)] 
    public required string AuthorPhone { get; set; }
    
    [StringLength(150, MinimumLength = 5)] 
    public required string AuthorEmail { get; set; }

    public bool HasAuthorDiscount { get; init; } = false;

    [StringLength(512, MinimumLength = 0)]
    public string? Comment { get; set; }

    public DeliveryWay DeliveryWay { get; init; } = DeliveryWay.Self;

    [StringLength(256, MinimumLength = 0)] 
    public string? DeliveryAddress { get; set; }

    public DateTime OrderDateTime { get; init; }

    public bool IsTestOrder { get; set; } = false;

    public OrderStatus Status { get; init; } = OrderStatus.Open;

    [StringLength(150, MinimumLength = 5)] 
    public string? PriorityOrderDate { get; set; }
}
