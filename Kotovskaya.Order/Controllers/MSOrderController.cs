using Microsoft.AspNetCore.Mvc;

namespace Kotovskaya.Order.Controllers
{
    [ApiController]
    [Route("api/order")]
    public class MSOrderController : ControllerBase
    {
        private readonly ILogger<MSOrderController> _logger;

        public MSOrderController(ILogger<MSOrderController> logger)
        {
            _logger = logger;
        }

        [HttpGet, Route("create")]
        public IEnumerable<string> Get()
        {
            return ["order", "created"];
        }
    }
}
