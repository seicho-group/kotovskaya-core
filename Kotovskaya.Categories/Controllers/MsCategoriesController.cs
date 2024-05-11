using Kotovskaya.Categories.Application.Services.GetCategoryItems;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Kotovskaya.Categories.Controllers
{
    
    [ApiController]
    [Route("api/categories")]
    public class MsCategoriesController : ControllerBase
    {

        private readonly ILogger<MsCategoriesController> _logger;
        private readonly IMediator _mediatr;

        public MsCategoriesController(ILogger<MsCategoriesController> logger, IMediator mediatr)
        {
            _logger = logger;
            _mediatr = mediatr;
        }

        [HttpPost, Route("get_category_items")]
        public async Task<object> GetCategoryItems([FromBody] GetCategoryItemsRequest request) =>
            Ok(await _mediatr.Send(request));
    }
}
