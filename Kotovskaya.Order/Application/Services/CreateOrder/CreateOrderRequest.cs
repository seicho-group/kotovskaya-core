using MediatR;

namespace Kotovskaya.Order.Application.Services.CreateOrder;

public record CreateOrderRequest : IRequest<string>
{
    public string Price { get; init; } = "0";
}