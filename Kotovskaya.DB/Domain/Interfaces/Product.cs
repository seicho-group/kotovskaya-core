namespace Kotovskaya.DB.Domain.Interfaces;

public class Product
{
    public string Id { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public Guid MSId { get; init; }
    public string Article { get; init; }
    public int Quantity { get; init; }
    public Category Category { get; init; }
    public string ImageLink { get; set; }
}