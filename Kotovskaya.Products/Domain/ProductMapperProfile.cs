using AutoMapper;
using Kotovskaya.DB.Domain.Entities;
using Kotovskaya.Products.Domain.Entities;

namespace Kotovskaya.Products.Domain;

public class ProductMapperProfile : Profile
{
    public ProductMapperProfile()
    {
        CreateMap<ProductEntity, ProductEntityDto>()
            .ReverseMap();
    }
}