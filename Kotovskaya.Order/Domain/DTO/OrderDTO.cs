namespace Kotovskaya.Order.Controllers.Order.Controllers.Categories.Application.Services.GetCategoryItems.Order.Domain.DTO;

public record OrderDTO(Guid OrderId, int Price, Guid OrdererID, string[]? Positions) : IOrderDTO;