using Kotovskaya.Order.Domain.DTO;
using MediatR;

namespace Kotovskaya.Order.Application.Services.GetOrder;

public class GetOrderHandler: IRequestHandler<GetOrderRequest, GetOrderResponse>
{
    public async Task<GetOrderResponse> Handle(GetOrderRequest request, CancellationToken cancellationToken)
    {
        await Task.Delay(200);
        var order = new OrderDTO(Guid.NewGuid(), 0, Guid.NewGuid(), new[] {"123"});
        return new GetOrderResponse(order);
    }
}