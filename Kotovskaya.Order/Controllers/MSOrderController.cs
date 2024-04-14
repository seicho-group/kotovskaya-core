using Kotovskaya.Order.Application.Services.CreateOrder;
using Kotovskaya.Order.Application.Services.GetOrder;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Kotovskaya.Order.Controllers
{
    [ApiController]
    [Route("api/order")]
    public class MSOrderController : ControllerBase
    {
        private readonly ILogger<MSOrderController> _logger;
        private readonly IMediator _mediator;
        public MSOrderController(ILogger<MSOrderController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost, Route("create_order")]
        public async Task<string> CreateOrder([FromBody] CreateOrderRequest request, CancellationToken cancellationToken) =>
            await _mediator.Send(request, cancellationToken);

        [HttpGet, Route("get_order")]
        public async Task<GetOrderResponse> GetOrder([FromQuery] GetOrderRequest request, CancellationToken cancellationToken) =>
            await _mediator.Send(request, cancellationToken);
        
    }
}
