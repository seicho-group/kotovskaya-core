using Confiti.MoySklad.Remap.Client;
using Kotovskaya.DB.Domain.Context;
using Kotovskaya.Products.Domain.Entities;
using MediatR;

namespace Kotovskaya.Products.Application.Services.GetSaleProducts;

public class GetSaleProductsHandler(KotovskayaMsContext msContext)
    : IRequestHandler<GetSaleProductsRequest, List<ProductEntityDto>>
{
    public async Task<List<ProductEntityDto>> Handle(GetSaleProductsRequest request, CancellationToken cancellationToken)
    {
        var query = new AssortmentApiParameterBuilder();
        query.Limit(10);
        await msContext.Assortment.GetAllAsync(query);
        
        return [];
    }
}