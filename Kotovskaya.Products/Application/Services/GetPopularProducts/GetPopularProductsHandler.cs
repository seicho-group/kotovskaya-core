﻿using AutoMapper;
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
            .Include(pr => pr.SaleTypes)
            .Where(pr => pr.MsId != null && pr.Quantity > 0 && newProductsIds
                .Contains(pr.MsId.ToString() ?? string.Empty))
            .OrderByDescending(pr => pr.Quantity)
            .ToListAsync(cancellationToken);

        return mapper.Map<List<ProductEntityDto>>(products);
    }
}
