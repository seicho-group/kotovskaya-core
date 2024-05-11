namespace Kotovskaya.DB.Domain.Interfaces;

public enum CategoryType
{
    Soapmaking,
    Candles,
    Cosmetics
}

public class Category
{
    public string Id { get; init; }
    public string Name { get; init; }
    public string? MSId { get; init; }
    public Category? ParentCategory { get; init; }
    public bool? IsVisible { get; init; }
    public CategoryType? Type { get; init; }
    public List<Product> Products { get; set; }
}