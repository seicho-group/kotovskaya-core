using AutoMapper;
using AutoMapper.QueryableExtensions;
using Kotovskaya.DB.Domain.Context;
using Kotovskaya.Products.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kotovskaya.Products.Application.Services.GetProductInfo;

public class GetProductInfoHandler(KotovskayaDbContext dbContext, IMapper mapper): IRequestHandler<GetProductInfoRequest, ProductEntityDto>
{
    public async Task<ProductEntityDto?> Handle(GetProductInfoRequest request, CancellationToken cancellationToken)
    {
        return await dbContext.Products
            .ProjectTo<ProductEntityDto>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(pr => pr.Id == request.ProductId);
    }
}