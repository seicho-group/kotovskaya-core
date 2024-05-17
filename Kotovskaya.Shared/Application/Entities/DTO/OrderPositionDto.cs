namespace Kotovskaya.Shared.Application.Entities.DTO;

public class OrderPositionDto
{
    public required Guid ProductId { get; set; }
    public int Quantity { get; set; }
}