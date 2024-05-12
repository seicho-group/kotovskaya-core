using Kotovskaya.Products.Domain.Entities;
using MediatR;

namespace Kotovskaya.Products.Application.Services.GetNewProducts;

public class GetNewProductsRequest : IRequest<List<ProductEntityDto>>
{
    public string Id { get; set; }
}