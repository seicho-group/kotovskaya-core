using AutoMapper;
using Kotovskaya.DB.Domain.Entities;
using Kotovskaya.Shared.Application.ServiceConfiguration.Entities.DTO;

namespace Kotovskaya.Shared.Application.ServiceConfiguration.MapperProfiles;

public class ProductMapperProfile : Profile
{
    public ProductMapperProfile()
    {
        CreateMap<ProductEntity, ProductEntityDto>()
            .ReverseMap();
    }
}