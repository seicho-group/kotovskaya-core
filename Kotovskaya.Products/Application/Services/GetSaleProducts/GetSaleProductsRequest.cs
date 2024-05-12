using Kotovskaya.Products.Domain.Entities;
using MediatR;

namespace Kotovskaya.Products.Application.Services.GetSaleProducts;

public class GetSaleProductsRequest : IRequest<List<ProductEntityDto>>
{
    public string Id { get; set; }
}