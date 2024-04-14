namespace Kotovskaya.Order.Domain.DTO;

public interface IOrderDTO
{
    Guid OrderId { get; init; }
    int Price { get; init; }
    string[]? Positions { get; init; }
    Guid OrdererID { get; init; }
}