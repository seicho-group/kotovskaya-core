using AutoMapper;
using Kotovskaya.DB.Domain.Entities.DatabaseEntities;
using Kotovskaya.Shared.Application.Entities.DTO;

namespace Kotovskaya.Shared.Application.MapperProfiles;

public class ProductMapperProfile : Profile
{
    public ProductMapperProfile()
    {
        CreateMap<ProductEntity, ProductEntityDto>()
            .ForMember(productEntityDto => productEntityDto.SalePrice,
                conf => conf.MapFrom(pr =>
                    pr.SaleTypes != null ? pr.SaleTypes.Price : 0))
            .ForMember(productEntityDto => productEntityDto.OldPrice,
                conf =>
                    conf.MapFrom(pr =>
                        pr.SaleTypes != null && pr.SaleTypes.OldPrice != 0 ? pr.SaleTypes.OldPrice : null))
            .ReverseMap();
    }
}
