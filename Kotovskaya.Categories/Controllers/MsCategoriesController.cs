using Microsoft.AspNetCore.Mvc;

namespace Kotovskaya.Categories.Controllers
{
    public record GetCategoryItemsRequest(string Category);
    
    [ApiController]
    [Route("api/categories")]
    public class MsCategoriesController : ControllerBase
    {

        private readonly ILogger<MsCategoriesController> _logger;

        public MsCategoriesController(ILogger<MsCategoriesController> logger)
        {
            _logger = logger;
        }

        [HttpPost, Route("get_category_items")]
        public async Task<object> GetCategoryItems([FromBody] GetCategoryItemsRequest request)
        {
            await Task.Delay(12);
            throw new NotImplementedException();
        }
    }
}
