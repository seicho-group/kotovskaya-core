using MediatR;

namespace Kotovskaya.Products.Application.Services.GetPopularProducts;

public class GetPopularProductsHandler: IRequestHandler<GetPopularProductsRequest, GetPopularProductsResponse>
{
    public Task<GetPopularProductsResponse> Handle(GetPopularProductsRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}