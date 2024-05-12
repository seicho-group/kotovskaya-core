using Kotovskaya.Shared.Application.ServiceConfiguration.Entities.DTO;
using MediatR;

namespace Kotovskaya.Products.Application.Services.GetSaleProducts;

public class GetSaleProductsRequest : IRequest<List<ProductEntityDto>>
{
}