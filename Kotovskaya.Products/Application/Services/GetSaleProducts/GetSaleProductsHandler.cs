using AutoMapper;
using AutoMapper.QueryableExtensions;
using Kotovskaya.DB.Domain.Context;
using Kotovskaya.Products.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kotovskaya.Products.Application.Services.GetSaleProducts;

public class GetSaleProductsHandler(KotovskayaMsContext msContext, KotovskayaDbContext dbContext, IMapper mapper)
    : IRequestHandler<GetSaleProductsRequest, List<ProductEntityDto>>
{
    public async Task<List<ProductEntityDto>> Handle(GetSaleProductsRequest request, CancellationToken cancellationToken)
    {
        var newProductsIds = await msContext.FindProductsIdByMoySkladAttribute(MsAttributes.IsSale, true);
        
        var products =  await dbContext.Products
            .Where(pr => pr.MsId != null && newProductsIds
                .Contains(pr.MsId.ToString() ?? string.Empty))
            .ProjectTo<ProductEntityDto>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
        
        return products;
    }
}