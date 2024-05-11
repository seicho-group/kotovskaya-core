using MediatR;

namespace Kotovskaya.Products.Application.Services.GetProductInfo;

public class GetProductInfoHandler: IRequestHandler<GetProductInfoRequest, GetProductInfoResponse>
{
    public Task<GetProductInfoResponse> Handle(GetProductInfoRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}