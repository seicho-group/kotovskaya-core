using Kotovskaya.Order.Controllers.Order.Controllers.Categories.Application.Services.GetCategoryItems.Order.Application.Services.CreateOrder;
using Kotovskaya.Order.Controllers.Order.Controllers.Categories.Application.Services.GetCategoryItems.Order.Application.Services.GetOrder;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Kotovskaya.Order.Controllers
{
    [ApiController]
    [Route("api/order")]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IMediator _mediator;
        public OrderController(ILogger<OrderController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost, Route("create_order")]
        public async Task<string> CreateOrder([FromBody] CreateOrderRequest request, CancellationToken cancellationToken) =>
            await _mediator.Send(request, cancellationToken);

        // todo: get order details
        [HttpGet, Route("get_order")]
        public async Task<GetOrderResponse> GetOrder([FromQuery] GetOrderRequest request, CancellationToken cancellationToken) =>
            await _mediator.Send(request, cancellationToken);
        
    }
}
