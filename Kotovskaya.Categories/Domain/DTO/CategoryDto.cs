using Kotovskaya.DB.Domain.Entities;

namespace Kotovskaya.Categories.Domain.DTO;

public class CategoryDto
{
    public string Name { get; set; } 
    public string Id { get; set; }
    public CategoryType Type { get; set; }
}

public class CategoryDtoBranch : CategoryDto
{
    public List<CategoryDtoBranch>? CategoryItems { get; set; }
}