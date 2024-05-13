using MediatR;

namespace Kotovskaya.Order.Application.Services.GetOrder;

public record GetOrderRequest : IRequest<GetOrderResponse>
{
    private Guid OrderId { get; init; }
}