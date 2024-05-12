using AutoMapper;
using Kotovskaya.DB.Domain.Entities;
using Kotovskaya.Shared.Application.Entities.DTO;

namespace Kotovskaya.Shared.Application.MapperProfiles;

public class ProductMapperProfile : Profile
{
    public ProductMapperProfile()
    {
        CreateMap<ProductEntity, ProductEntityDto>()
            .ReverseMap();
    }
}