using AutoMapper;
using Kotovskaya.DB.Domain.Entities.DatabaseEntities;
using Kotovskaya.Shared.Application.Entities.DTO;

namespace Kotovskaya.Shared.Application.MapperProfiles;

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