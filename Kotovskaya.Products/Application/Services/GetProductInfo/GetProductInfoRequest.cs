using Kotovskaya.Shared.Application.ServiceConfiguration.Entities.DTO;
using MediatR;

namespace Kotovskaya.Products.Application.Services.GetProductInfo;

public class GetProductInfoRequest : IRequest<ProductEntityDto>
{
    public required string ProductId { get; set; }
}