using Kotovskaya.Shared.Application.ServiceConfiguration.Entities.DTO;
using MediatR;

namespace Kotovskaya.Order.Application.Services.GetOrder;

public class GetOrderHandler: IRequestHandler<GetOrderRequest, GetOrderResponse>
{
    public async Task<GetOrderResponse> Handle(GetOrderRequest request, CancellationToken cancellationToken)
    {
        await Task.Delay(200);
        var order = new OrderDto()
        {
            Id = Guid.NewGuid(), 
            Author = "Фед"
        };
        return new GetOrderResponse(order);
    }
}