using Kotovskaya.DB.Domain.Entities;
using MediatR;

namespace Kotovskaya.Order.Application.Services.CreateOrder;

public record CreateOrderRequest : IRequest<string>
{
    public required string AuthorName { get; set; }

    public required string AuthorPhone { get; set; }
    
    public required string AuthorMail { get; set; }

    public bool? HasAuthorDiscount { get; set; } = false;

    public string? Comment { get; set; }

    public DeliveryWay DeliveryWay { get; set; } = DeliveryWay.Self;
    
    public string? DeliveryAddress { get; set; }
    
    public DateTime? PriorityOrderDate { get; set; }
}