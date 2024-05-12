using MediatR;

namespace Kotovskaya.Products.Application.Services.GetNewProducts;

public class GetNewProductsRequest : IRequest<GetNewProductsResponse>
{
    public string Id { get; set; }
}