using Kotovskaya.Categories.Domain.DTO;

namespace Kotovskaya.Categories.Application.Services.GetAllCategoriesTree;

public record GetAllCategoriesTreeResponse
{
    public List<CategoryDtoBranch> Categories { get; init; } = null!;
}