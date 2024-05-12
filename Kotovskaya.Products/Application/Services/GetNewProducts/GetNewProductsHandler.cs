using Kotovskaya.Products.Domain.Entities;
using MediatR;

namespace Kotovskaya.Products.Application.Services.GetNewProducts;

public class GetNewProductsHandler: IRequestHandler<GetNewProductsRequest, List<ProductEntityDto>>
{
    public Task<List<ProductEntityDto>> Handle(GetNewProductsRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}