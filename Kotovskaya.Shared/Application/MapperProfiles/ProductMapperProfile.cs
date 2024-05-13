using AutoMapper;
using Kotovskaya.DB.Domain.Entities.DatabaseEntities;
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
