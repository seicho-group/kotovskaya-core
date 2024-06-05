using MediatR;

namespace Kotovskaya.Categories.Application.Services.GetCategoryItems;

public class GetCategoryItemsRequest : IRequest<GetCategoryItemsResponse>
{
    public required Guid CategoryId { get; init; }
}
