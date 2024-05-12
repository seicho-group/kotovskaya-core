using Kotovskaya.Products.Domain.Entities;
using MediatR;

namespace Kotovskaya.Products.Application.Services.GetPopularProducts;

public class GetPopularProductsHandler: IRequestHandler<GetPopularProductsRequest, List<ProductEntityDto>>
{
    public Task<List<ProductEntityDto>> Handle(GetPopularProductsRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}