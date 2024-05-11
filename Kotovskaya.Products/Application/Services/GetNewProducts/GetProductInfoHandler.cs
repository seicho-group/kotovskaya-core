using MediatR;

namespace Kotovskaya.Products.Application.Services.GetNewProducts;

public class GetNewProductsHandler: IRequestHandler<GetNewProductsRequest, GetNewProductsResponse>
{
    public Task<GetNewProductsResponse> Handle(GetNewProductsRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}