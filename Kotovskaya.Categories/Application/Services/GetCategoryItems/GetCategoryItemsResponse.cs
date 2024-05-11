using Kotovskaya.Categories.Domain.DTO;

namespace Kotovskaya.Categories.Application.Services.GetCategoryItems;

public record GetCategoryItemsResponse
{
    public string CategoryName { get; init; } = "Категория";
    
    public string CategoryId { get; init; } = Guid.Empty.ToString();
    
    public ProductEntityDto[] CategoryItems { get; init; } = null!;
    
    public CategoryDto[] CategoryChilds { get; init; } = null!;
}