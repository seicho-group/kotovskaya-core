using AutoMapper;
using Kotovskaya.DB.Domain.Entities;

namespace Kotovskaya.Categories.Domain.DTO;

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

