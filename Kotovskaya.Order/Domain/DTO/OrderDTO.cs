namespace Kotovskaya.Order.Domain.DTO;

public record OrderDTO(Guid OrderId, int Price, Guid OrdererID, string[]? Positions) : IOrderDTO;