using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Kotovskaya.Products.Controllers
{
    [ApiController]
    [Route("api/order")]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IMediator _mediator;
        public ProductsController(ILogger<ProductsController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }
    }
}