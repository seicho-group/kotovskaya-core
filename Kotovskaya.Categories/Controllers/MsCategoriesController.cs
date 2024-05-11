using Confiti.MoySklad.Remap.Api;
using Confiti.MoySklad.Remap.Client;
using Confiti.MoySklad.Remap.Entities;
using Confiti.MoySklad.Remap.Models;
using Kotovskaya.DB.Domain.Context;
using Kotovskaya.DB.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Product = Kotovskaya.DB.Domain.Interfaces.Product;

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
        public async  Task<object> GetCategoryItems([FromBody] GetCategoryItemsRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
