using AutoMapper;
using AutoMapper.QueryableExtensions;
using Kotovskaya.DB.Application.Services.UpdatingDataController.cs;
using Kotovskaya.DB.Domain.Context;
using Kotovskaya.Shared.Application.Entities.DTO;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kotovskaya.Products.Application.Services.GetPopularProducts;

public class GetPopularProductsHandler(KotovskayaMsContext msContext, KotovskayaDbContext dbContext, IMapper mapper)
    : IRequestHandler<GetPopularProductsRequest, List<ProductEntityDto>>
{
    public async Task<List<ProductEntityDto>> Handle(GetPopularProductsRequest request,
        CancellationToken cancellationToken)
    {
        var newProductsIds = await msContext.FindProductsIdByMoySkladAttribute(MsAttributes.IsPopular, true);

        var products = await dbContext.Products
            .Where(pr => pr.MsId != null && newProductsIds
                .Contains(pr.MsId.ToString() ?? string.Empty))
            .OrderByDescending(pr => pr.Quantity)
            .ProjectTo<ProductEntityDto>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        await new UpdatingDataController(msContext, dbContext).UpdateProductData(products.Select(pr => pr.Id).ToList());

        return products;
    }
}
