using AutoMapper;
using Kotovskaya.DB.Application.Services.UpdatingDataController.cs;
using Kotovskaya.DB.Domain.Context;
using Kotovskaya.Shared.Application.Entities.DTO;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kotovskaya.Products.Application.Services.GetNewProducts;

public class GetNewProductsHandler(KotovskayaMsContext msContext, KotovskayaDbContext dbContext, IMapper mapper)
    : IRequestHandler<GetNewProductsRequest, List<ProductEntityDto>>
{
    public async Task<List<ProductEntityDto>> Handle(GetNewProductsRequest request, CancellationToken cancellationToken)
    {
        var newProductsIds = await msContext.FindProductsIdByMoySkladAttribute(MsAttributes.IsNew, true);

        var products = await dbContext.Products
            .Include(pr => pr.SaleTypes)
            .Where(pr => pr.MsId != null && pr.Quantity > 0 && newProductsIds
                .Contains(pr.MsId.ToString() ?? string.Empty))
            .OrderByDescending(pr => pr.Quantity)
            .ToListAsync(cancellationToken);

        await new UpdatingDataController(msContext, dbContext).UpdateProductData(products.ToList());

        return mapper.Map<List<ProductEntityDto>>(products);
    }
}
