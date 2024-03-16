using Microsoft.AspNetCore.Mvc;

namespace Kotovskaya.Order.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MSOrderController : ControllerBase
    {
        private readonly ILogger<MSOrderController> _logger;

        public MSOrderController(ILogger<MSOrderController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "createOrder")]
        public IEnumerable<string> Get()
        {
            return ["order", "created"];
        }
    }
}
