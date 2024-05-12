using Kotovskaya.Shared.Application.ServiceConfiguration.Entities.DTO;
using MediatR;

namespace Kotovskaya.Products.Application.Services.GetNewProducts;

public class GetNewProductsRequest : IRequest<List<ProductEntityDto>>
{
}