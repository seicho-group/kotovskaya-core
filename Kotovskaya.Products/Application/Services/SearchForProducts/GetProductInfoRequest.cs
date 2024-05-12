using Kotovskaya.Shared.Application.ServiceConfiguration.Entities.DTO;
using MediatR;

namespace Kotovskaya.Products.Application.Services.SearchForProducts;

public class SearchForProductsRequest : IRequest<List<ProductEntityDto>>
{
    public required string SearchString { get; set; }
    public int Limit { get; set; } = 8;
    public int Page { get; set; } = 1;
}