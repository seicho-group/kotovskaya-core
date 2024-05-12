using Kotovskaya.Products.Domain.Entities;
using MediatR;

namespace Kotovskaya.Products.Application.Services.GetProductInfo;

public class GetProductInfoRequest : IRequest<ProductEntityDto>
{
    public string ProductId { get; set; }
}