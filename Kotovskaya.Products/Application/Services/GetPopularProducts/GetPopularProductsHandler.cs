using Confiti.MoySklad.Remap.Client;
using Kotovskaya.DB.Domain.Context;
using Kotovskaya.Products.Domain.Entities;
using MediatR;

namespace Kotovskaya.Products.Application.Services.GetPopularProducts;

public class GetPopularProductsHandler(KotovskayaMsContext msContext)
    : IRequestHandler<GetPopularProductsRequest, List<ProductEntityDto>>
{
    public async Task<List<ProductEntityDto>> Handle(GetPopularProductsRequest request, CancellationToken cancellationToken)
    {
        var query = new AssortmentApiParameterBuilder();
        query.Limit(10);
        await msContext.Assortment.GetAllAsync(query);
        
        return [];
    }
}