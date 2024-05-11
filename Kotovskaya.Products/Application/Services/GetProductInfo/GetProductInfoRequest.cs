using MediatR;

namespace Kotovskaya.Products.Application.Services.GetProductInfo;

public class GetProductInfoRequest : IRequest<GetProductInfoResponse>
{
    public string Id { get; set; }
}