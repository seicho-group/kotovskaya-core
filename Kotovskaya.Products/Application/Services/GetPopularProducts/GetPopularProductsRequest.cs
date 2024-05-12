using MediatR;

namespace Kotovskaya.Products.Application.Services.GetPopularProducts;

public class GetPopularProductsRequest : IRequest<GetPopularProductsResponse>
{
    public string Id { get; set; }
}