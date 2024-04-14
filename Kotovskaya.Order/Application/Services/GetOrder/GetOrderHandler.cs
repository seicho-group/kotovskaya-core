using MediatR;

namespace Kotovskaya.Order.Application.Services.GetOrder;

public class GetOrderHandler: IRequestHandler<GetOrderRequest, GetOrderResponse>
{
    public Task<GetOrderResponse> Handle(GetOrderRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}