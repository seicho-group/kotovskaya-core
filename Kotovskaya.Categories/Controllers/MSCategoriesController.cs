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
    public class MSCategoriesController : ControllerBase
    {

        private readonly ILogger<MSCategoriesController> _logger;

        public MSCategoriesController(ILogger<MSCategoriesController> logger)
        {
            _logger = logger;
        }

        [HttpPost, Route("get_category_items")]
        public async  Task<object> GetCategoryItems([FromBody] GetCategoryItemsRequest request)
        {
            var credentials = new MoySkladCredentials()
            {
                AccessToken = "0f165f456ecc4bf2fa2f189b69be402f4dc430d8"
            };
            using (var db = new KotovskayaDbContext())
            {
                var msApi = new MoySkladApi(credentials);
                var folders = await msApi.ProductFolder.GetAllAsync();
                foreach (var folder in folders.Payload.Rows)
                {
                    if (folder.PathName == "")
                    {
                        var category = new Category {Id = folder.Id?.ToString() ?? Guid.NewGuid().ToString(), Name = folder.Name, MSId = folder.Id?.ToString() };    
                        db.Categories.Add(category);
                    }
                }

                await db.SaveChangesAsync();
            }
            
            return Ok();
        }
    }
}
