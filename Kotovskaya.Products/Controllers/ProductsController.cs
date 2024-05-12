using Kotovskaya.Products.Application.Services.GetNewProducts;
using Kotovskaya.Products.Application.Services.GetPopularProducts;
using Kotovskaya.Products.Application.Services.GetProductInfo;
using Kotovskaya.Products.Application.Services.GetSaleProducts;
using Kotovskaya.Products.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Kotovskaya.Products.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IMediator _mediator;
        public ProductsController(ILogger<ProductsController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost, Route("get_product_info")]
        public async Task<ActionResult<GetProductInfoResponse>> GetProductInfo([FromBody] GetProductInfoRequest request,
            CancellationToken cancellationToken) => Ok( await _mediator.Send(request, cancellationToken));

        [HttpPost, Route("get_new_products")]
        public async Task<ActionResult<List<ProductEntityDto>>> GetNewProducts([FromRoute] GetNewProductsRequest request,
            CancellationToken cancellationToken) => Ok( await _mediator.Send(request, cancellationToken));
        
        
        [HttpPost, Route("get_popular_products")]
        public async Task<ActionResult<List<ProductEntityDto>>> GetPopularProducts([FromBody] GetPopularProductsRequest request,
            CancellationToken cancellationToken) => Ok( await _mediator.Send(request, cancellationToken));
        
        [HttpPost, Route("get_sale_products")]
        public async Task<ActionResult<List<ProductEntityDto>>> GetSaleProducts([FromBody] GetSaleProductsRequest request,
            CancellationToken cancellationToken) => Ok( await _mediator.Send(request, cancellationToken));
    }
}