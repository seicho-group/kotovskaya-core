using Kotovskaya.Categories.Application.Services.GetAllCategoriesTree;
using Kotovskaya.Categories.Application.Services.GetCategoryItems;
using Kotovskaya.Shared.Application.Entities.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Kotovskaya.Categories.Controllers;

[ApiController]
[Route("api/categories")]
public class MsCategoriesController(IMediator mediatr) : ControllerBase
{
    [HttpPost]
    [Route("get_category_items")]
    public async Task<ActionResult<GetCategoryItemsResponse>> GetCategoryItems(
        [FromBody] GetCategoryItemsRequest request)
    {
        return Ok(await mediatr.Send(request));
    }

    [HttpGet]
    [Route("get_all_categories_tree")]
    public async Task<ActionResult<List<CategoryDtoBranch>>> GetAllCategoriesTree(
        [FromRoute] GetAllCategoriesTreeRequest request)
    {
        return Ok(await mediatr.Send(request));
    }
}