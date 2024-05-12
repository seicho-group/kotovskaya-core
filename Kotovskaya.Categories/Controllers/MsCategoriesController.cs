using Kotovskaya.Categories.Application.Services.GetAllCategoriesTree;
using Kotovskaya.Categories.Application.Services.GetCategoryItems;
using Kotovskaya.Shared.Application.ServiceConfiguration.Entities.DTO;
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
        public async Task<ActionResult<GetCategoryItemsResponse>> GetCategoryItems([FromBody] GetCategoryItemsRequest request) =>
            Ok(await _mediatr.Send(request));

        [HttpGet, Route("get_all_categories_tree")]
        public async Task<ActionResult<List<CategoryDtoBranch>>> GetAllCategoriesTree([FromRoute] GetAllCategoriesTreeRequest request) =>
            Ok(await _mediatr.Send(request));
    }
}
