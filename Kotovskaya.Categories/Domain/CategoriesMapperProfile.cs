using AutoMapper;
using Kotovskaya.Categories.Domain.DTO;
using Kotovskaya.DB.Domain.Entities;

namespace Kotovskaya.Categories.Domain;

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

