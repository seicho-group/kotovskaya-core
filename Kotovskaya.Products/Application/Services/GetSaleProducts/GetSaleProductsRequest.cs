using MediatR;

namespace Kotovskaya.Products.Application.Services.GetSaleProducts;

public class GetSaleProductsRequest : IRequest<GetSaleProductsResponse>
{
    public string Id { get; set; }
}