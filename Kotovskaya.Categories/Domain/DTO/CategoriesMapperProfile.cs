using AutoMapper;
using Kotovskaya.DB.Domain.Entities;

namespace Kotovskaya.Categories.Domain.DTO;

public class CategoriesMapperProfile : Profile
{
    public CategoriesMapperProfile()
    {
        CreateMap<Category, CategoryDto>()
            .ForMember(catDto => catDto.Name,
                cfg => cfg.MapFrom(cat => cat.Name));
    }
}

