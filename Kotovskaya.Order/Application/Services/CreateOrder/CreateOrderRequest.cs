using MediatR;

namespace Kotovskaya.Order.Controllers.Order.Controllers.Categories.Application.Services.GetCategoryItems.Order.Application.Services.CreateOrder;

public record CreateOrderRequest : IRequest<string>
{
    public string Price { get; init; } = "0";
}