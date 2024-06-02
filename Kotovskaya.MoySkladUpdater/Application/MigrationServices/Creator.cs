using Confiti.MoySklad.Remap.Entities;
using Kotovskaya.DB.Domain.Context;
using Kotovskaya.DB.Domain.Entities.DatabaseEntities;
using Kotovskaya.DB.Domain.Entities.MoySkladExtensions;

namespace Kotovskaya.MoySkladUpdater.Application.MigrationServices;

public class Creator(KotovskayaDbContext dbContext)
{
    public async Task CreateProduct(KotovskayaAssortment product, Category category)
    {
        try
        {
            var desc = product.Description;
            var productEntity = new ProductEntity
            {
                Category = category,
                CategoryId = category.Id,
                // проверяем на наличие в where
                MsId = (Guid)product.Id!,
                Name = product.Name,
                Description = desc[..(desc.Length > 2040 ? 2040 : desc.Length)],
                Quantity = (int)(product.Quantity ?? 0),
                Article = null
            };
            await dbContext.Products.AddAsync(productEntity);
        }
        catch (Exception e)
        {
            SentrySdk.CaptureException(e);
        }
    }

    public async Task CreateCategory(Group category)
    {
        try
        {
            var categoryEntity = new Category
            {
                Name = category.Name,
                Type = CategoryType.Soapmaking,
                MsId = (Guid)category.Id!
            };
            await dbContext.Categories.AddAsync(categoryEntity);
        }
        catch (Exception e)
        {
            SentrySdk.CaptureException(e);
        }
    }
}
