using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kotovskaya.DB.Domain.Entities;

public enum CategoryType
{
    Soapmaking,
    Candles,
    Cosmetics
}

public class Category
{
    [StringLength(150, MinimumLength = 5)]
    public string Id { get; init; } = new Guid().ToString();

    [StringLength(150, MinimumLength = 5)] 
    public string Name { get; init; } = "Категория";
    
    [StringLength(150, MinimumLength = 5)]
    public string? MsId { get; init; }
    
    [StringLength(150, MinimumLength = 5)]
    public string? ParentCategoryId { get; init; }
    public Category? ParentCategory { get; init; }

    public List<ProductEntity>? Products { get; init; } = null;
    
    public bool? IsVisible { get; init; }
    
    public CategoryType? Type { get; init; }
}