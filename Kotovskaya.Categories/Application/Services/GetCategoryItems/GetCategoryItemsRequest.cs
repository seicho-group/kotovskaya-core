using MediatR;

namespace Kotovskaya.Categories.Application.Services.GetCategoryItems;

public class GetCategoryItemsRequest:IRequest<GetCategoryItemsResponse>
{
    public required string CategoryId { get; init; }
}