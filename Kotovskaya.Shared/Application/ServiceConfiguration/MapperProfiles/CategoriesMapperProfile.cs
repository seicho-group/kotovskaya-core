using AutoMapper;
using Kotovskaya.DB.Domain.Entities;
using Kotovskaya.Shared.Application.ServiceConfiguration.Entities.DTO;

namespace Kotovskaya.Shared.Application.ServiceConfiguration.MapperProfiles;

public class CategoriesMapperProfile : Profile
{
    public CategoriesMapperProfile()
    {
        CreateMap<Category, CategoryDto>()
            .ReverseMap();
        
        CreateMap<Category, CategoryDtoBranch>()
            .ReverseMap();
    }
}

