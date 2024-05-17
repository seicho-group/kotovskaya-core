using Kotovskaya.Shared.Application.Entities.DTO;
using MediatR;

namespace Kotovskaya.Products.Application.Services.GetSaleProducts;

public class GetSaleProductsRequest : IRequest<List<ProductEntityDto>>
{
}