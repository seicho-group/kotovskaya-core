using Kotovskaya.Products.Application.Services.CatchProductUpdate;
using Kotovskaya.Products.Application.Services.GenerateFeed;
using Kotovskaya.Products.Application.Services.GetNewProducts;
using Kotovskaya.Products.Application.Services.GetPopularProducts;
using Kotovskaya.Products.Application.Services.GetProductInfo;
using Kotovskaya.Products.Application.Services.GetSaleProducts;
using Kotovskaya.Products.Application.Services.SearchForProducts;
using Kotovskaya.Shared.Application.Entities.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Kotovskaya.Products.Controllers;

[ApiController]
[Route("api/products")]
public class ProductsController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    [Route("get_product_info")]
    public async Task<ActionResult<ProductEntityDto>> GetProductInfo([FromBody] GetProductInfoRequest request,
        CancellationToken cancellationToken)
    {
        return Ok(await mediator.Send(request, cancellationToken));
    }

    [HttpPost]
    [Route("search_for_products")]
    public async Task<ActionResult<ProductEntityDto>> SearchForProducts([FromBody] SearchForProductsRequest request,
        CancellationToken cancellationToken)
    {
        return Ok(await mediator.Send(request, cancellationToken));
    }

    [HttpPost]
    [Route("get_new_products")]
    public async Task<ActionResult<List<ProductEntityDto>>> GetNewProducts([FromRoute] GetNewProductsRequest request,
        CancellationToken cancellationToken)
    {
        return Ok(await mediator.Send(request, cancellationToken));
    }


    [HttpPost]
    [Route("get_popular_products")]
    public async Task<ActionResult<List<ProductEntityDto>>> GetPopularProducts(
        [FromRoute] GetPopularProductsRequest request,
        CancellationToken cancellationToken)
    {
        return Ok(await mediator.Send(request, cancellationToken));
    }

    [HttpPost]
    [Route("get_sale_products")]
    public async Task<ActionResult<List<ProductEntityDto>>> GetSaleProducts([FromRoute] GetSaleProductsRequest request,
        CancellationToken cancellationToken)
    {
        return Ok(await mediator.Send(request, cancellationToken));
    }

    [HttpPost]
    [Route("catch_product_update")]
    public async Task CatchProductUpdate([FromBody] CatchProductUpdateRequest request,
        CancellationToken cancellationToken)
    {
        await mediator.Send(request, cancellationToken);
    }

    [HttpPost]
    [Route("catch_product_create")]
    public async Task CatchProductCreate([FromBody] CatchProductUpdateRequest request,
        CancellationToken cancellationToken)
    {
        await mediator.Send(request, cancellationToken);
    }

    [HttpGet]
    [Route("generative_feed")]
    public async Task<ActionResult> GenerateFeed(CancellationToken cancellationToken)
    {
        return Content(await mediator.Send(new GenerateFeedRequest(), cancellationToken), "application/xml");
    }
}
