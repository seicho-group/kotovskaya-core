using Kotovskaya.Shared.Application.Entities.DTO;

namespace Kotovskaya.Categories.Application.Services.GetCategoryItems;

public record GetCategoryItemsResponse
{
    public string CategoryName { get; init; } = "Категория";

    public Guid CategoryId { get; init; } = Guid.Empty;

    public ProductEntityDto[] CategoryItems { get; init; } = null!;

    public CategoryDto[] CategoryChildren { get; init; } = null!;
}
