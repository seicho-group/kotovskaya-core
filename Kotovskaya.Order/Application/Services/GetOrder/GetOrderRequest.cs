using MediatR;

namespace Kotovskaya.Order.Controllers.Order.Controllers.Categories.Application.Services.GetCategoryItems.Order.Application.Services.GetOrder;

public record GetOrderRequest : IRequest<GetOrderResponse>
{
    private Guid OrderId { get; init; }
}