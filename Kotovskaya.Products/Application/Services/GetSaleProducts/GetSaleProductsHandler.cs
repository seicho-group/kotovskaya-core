using Kotovskaya.Products.Domain.Entities;
using MediatR;

namespace Kotovskaya.Products.Application.Services.GetSaleProducts;

public class GetSaleProductsHandler: IRequestHandler<GetSaleProductsRequest, List<ProductEntityDto>>
{
    public Task<List<ProductEntityDto>> Handle(GetSaleProductsRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}