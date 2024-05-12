namespace Kotovskaya.Shared.Application.Entities.DTO;

public class ProductEntityDto
{
    public required string Id { get; init; }
    public required string Name { get; init; }
    public string? Description { get; init; }
    public int Quantity { get; init; }
}