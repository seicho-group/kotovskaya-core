namespace Kotovskaya.Shared.Application.ServiceConfiguration.Entities.DTO;

public class OrderDto
{
    public required Guid Id { get; init; }
    public required string Author { get; init; }
    public string? Description { get; init; }
    public int Quantity { get; init; }
}