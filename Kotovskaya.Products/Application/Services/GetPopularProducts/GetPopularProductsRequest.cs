using Kotovskaya.Shared.Application.ServiceConfiguration.Entities.DTO;
using MediatR;

namespace Kotovskaya.Products.Application.Services.GetPopularProducts;

public class GetPopularProductsRequest : IRequest<List<ProductEntityDto>>
{
}