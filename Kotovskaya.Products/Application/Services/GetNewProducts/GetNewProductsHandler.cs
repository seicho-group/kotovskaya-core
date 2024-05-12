using AutoMapper;
using AutoMapper.QueryableExtensions;
using Kotovskaya.DB.Domain.Context;
using Kotovskaya.Shared.Application.ServiceConfiguration.Entities.DTO;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kotovskaya.Products.Application.Services.GetNewProducts;

public class GetNewProductsHandler(KotovskayaMsContext msContext, KotovskayaDbContext dbContext, IMapper mapper)
    : IRequestHandler<GetNewProductsRequest, List<ProductEntityDto>>
{
    public async Task<List<ProductEntityDto>> Handle(GetNewProductsRequest request, CancellationToken cancellationToken)
    {
        var newProductsIds = await msContext.FindProductsIdByMoySkladAttribute(MsAttributes.IsNew, true);
        
        var products =  await dbContext.Products
            .Where(pr => pr.MsId != null && newProductsIds
                .Contains(pr.MsId.ToString() ?? string.Empty))
            .ProjectTo<ProductEntityDto>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
        
        return products;
    }
}