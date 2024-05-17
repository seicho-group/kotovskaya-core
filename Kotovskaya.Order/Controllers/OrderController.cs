using Kotovskaya.Order.Application.Services.CreateOrder;
using Kotovskaya.Order.Application.Services.GetOrder;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Kotovskaya.Order.Controllers;

[ApiController]
[Route("api/order")]
public class OrderController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    [Route("create_order")]
    public async Task<string> CreateOrder([FromBody] CreateOrderRequest request, CancellationToken cancellationToken)
    {
        return await mediator.Send(request, cancellationToken);
    }

    // todo: get order details
    [HttpGet]
    [Route("get_order")]
    public async Task<GetOrderResponse> GetOrder([FromQuery] GetOrderRequest request,
        CancellationToken cancellationToken)
    {
        return await mediator.Send(request, cancellationToken);
    }
}