using Kotovskaya.DB.Domain.Entities;

namespace Kotovskaya.Shared.Application.Entities.DTO;

public class CategoryDto
{
    public required string Name { get; set; } 
    public required string Id { get; set; }
    public CategoryType Type { get; set; }
}

public class CategoryDtoBranch : CategoryDto
{
    public List<CategoryDtoBranch>? CategoryItems { get; set; }
}