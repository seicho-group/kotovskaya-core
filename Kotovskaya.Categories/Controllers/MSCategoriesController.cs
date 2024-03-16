using Microsoft.AspNetCore.Mvc;

namespace Kotovskaya.Categories.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class MSCategoriesController : ControllerBase
    {

        private readonly ILogger<MSCategoriesController> _logger;

        public MSCategoriesController(ILogger<MSCategoriesController> logger)
        {
            _logger = logger;
        }

        [HttpGet, Route("get_all")]
        public string Get()
        {
            return "created succesfully";
        }
    }
}
