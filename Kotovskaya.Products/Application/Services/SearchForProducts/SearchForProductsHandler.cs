using AutoMapper;
using AutoMapper.QueryableExtensions;
using Kotovskaya.DB.Domain.Context;
using Kotovskaya.Shared.Application.ServiceConfiguration.Entities.DTO;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kotovskaya.Products.Application.Services.SearchForProducts;

public class SearchForProductsHandler(KotovskayaDbContext dbContext, IMapper mapper): IRequestHandler<SearchForProductsRequest, List<ProductEntityDto>>
{
    public async Task<List<ProductEntityDto>> Handle(SearchForProductsRequest request, CancellationToken cancellationToken)
    {
        return await dbContext.Products
            .Where(pr => pr.Name
                .ToLower()
                .Contains(request.SearchString.ToLower()))
            .Take(request.Limit)
            .Skip(request.Limit * (request.Page - 1))
            .ProjectTo<ProductEntityDto>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}   