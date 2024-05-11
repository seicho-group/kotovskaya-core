namespace Kotovskaya.Order.Controllers.Order.Controllers.Categories.Application.Services.GetCategoryItems.Order.Domain.DTO;

public interface IOrderDTO
{
    Guid OrderId { get; init; }
    int Price { get; init; }
    string[]? Positions { get; init; }
    Guid OrdererID { get; init; }
}