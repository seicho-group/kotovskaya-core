using Kotovskaya.Shared.Application.Entities.DTO;
using MediatR;

namespace Kotovskaya.Products.Application.Services.GetNewProducts;

public class GetNewProductsRequest : IRequest<List<ProductEntityDto>>
{
}