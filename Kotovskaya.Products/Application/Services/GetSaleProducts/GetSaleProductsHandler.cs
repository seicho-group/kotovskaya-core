using MediatR;

namespace Kotovskaya.Products.Application.Services.GetSaleProducts;

public class GetSaleProductsHandler: IRequestHandler<GetSaleProductsRequest, GetSaleProductsResponse>
{
    public Task<GetSaleProductsResponse> Handle(GetSaleProductsRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}