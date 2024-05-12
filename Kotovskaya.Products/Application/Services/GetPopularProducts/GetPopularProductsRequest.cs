using Kotovskaya.Products.Domain.Entities;
using MediatR;

namespace Kotovskaya.Products.Application.Services.GetPopularProducts;

public class GetPopularProductsRequest : IRequest<List<ProductEntityDto>>
{
    public string Id { get; set; }
}