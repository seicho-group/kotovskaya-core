using AspNetCore.Yandex.ObjectStorage;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Confiti.MoySklad.Remap.Client;
using Kotovskaya.DB.Domain.Context;
using Kotovskaya.Shared.Application.Entities.DTO;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kotovskaya.Products.Application.Services.GetProductInfo;

public class GetProductInfoHandler(KotovskayaDbContext dbContext, IMapper mapper,YandexStorageService yandexStorageService)
    : IRequestHandler<GetProductInfoRequest, ProductEntityDto>
{
    public async Task<ProductEntityDto> Handle(GetProductInfoRequest request, CancellationToken cancellationToken)
    {
        var res = await yandexStorageService.ObjectService.GetAsync("photo_2024-05-01_23-24-49.jpg");

        Console.WriteLine(res.StatusCode.ToString());
        var product = await dbContext.Products
            .ProjectTo<ProductEntityDto>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(pr => pr.Id == request.ProductId, cancellationToken);

        if (product == null) throw new ApiException(404, "Продукт с данным ID не найден");

        return product;
    }
}
