using Kotovskaya.Order.Domain.DTO;

namespace Kotovskaya.Order.Application.Services.GetOrder;

public record GetOrderResponse(IOrderDTO Order);