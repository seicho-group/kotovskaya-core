using Microsoft.AspNetCore.Mvc;

namespace Kotovskaya.Categories.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MSCategoriesController : ControllerBase
    {

        private readonly ILogger<MSCategoriesController> _logger;

        public MSCategoriesController(ILogger<MSCategoriesController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "getCategories")]
        public string Get()
        {
            return "created succesfully";
        }
    }
}
