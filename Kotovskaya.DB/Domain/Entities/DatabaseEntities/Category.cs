using System.ComponentModel.DataAnnotations;

namespace Kotovskaya.DB.Domain.Entities.DatabaseEntities;

public enum CategoryType
{
    Soapmaking,
    Candles,
    Cosmetics
}

public class Category
{
    [StringLength(150, MinimumLength = 5)] public Guid Id { get; init; }

    [StringLength(150, MinimumLength = 5)] public string Name { get; init; } = "Категория";
    [StringLength(150, MinimumLength = 5)] public string PathName { get; init; } = "";

    [StringLength(150, MinimumLength = 5)] public Guid MsId { get; init; }

    [StringLength(150, MinimumLength = 5)] public Guid? ParentCategoryId { get; init; }

    public Category? ParentCategory { get; set; }

    public List<ProductEntity>? Products { get; init; } = null;

    public bool? IsVisible { get; init; } = true;

    public CategoryType? Type { get; init; }
}
