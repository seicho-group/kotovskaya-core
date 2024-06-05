using Confiti.MoySklad.Remap.Client;
using Kotovskaya.DB.Domain.Context;
using Kotovskaya.DB.Domain.Entities.DatabaseEntities;
using Kotovskaya.DB.Domain.Entities.MoySkladExtensions;
using Kotovskaya.MoySkladUpdater.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Kotovskaya.MoySkladUpdater.Application.MigrationServices;

public class ProductUpdater(
    KotovskayaMsContext msContext, 
    KotovskayaDbContext dbContext, 
    KotovskayaYandexObjectStorageContext yandexObjectStorageContext) : IMigrationService
{
    public async Task Migrate()
    {
        var categories = await dbContext.Categories.ToListAsync();
        foreach (var category in categories)
        {
            Console.WriteLine("Updating category " + category.Name);
            await ExecuteCategoryUpdate(category);
        }
    }

    private async Task ExecuteCategoryUpdate(Category category)
    {
        var currentCategoryProducts =  await dbContext.Products
            .Where(pr => pr.Categories.Any(cat => cat.MsId == category.MsId))
            .ToListAsync();

        // remove excess
        // update current
        // fetch not implemented
        try
        {
            var notImplementedProducts =
                await GetNotImplementedProducts(currentCategoryProducts.Select(pr => pr.MsId).ToList(), category.MsId);

            foreach (var notImplementedProduct in notImplementedProducts)
            {
                await new Creator(dbContext, msContext, yandexObjectStorageContext)
                    .CreateProductOrUpdateCategory(notImplementedProduct, category, currentCategoryProducts);
            }
        }
        catch (Exception e)
        {
            SentrySdk.CaptureMessage($"Failed to update not implemented products to category {category.MsId}");
        }

        await dbContext.SaveChangesAsync();
    }

    private async Task ExecuteProductUpdate(ProductEntity productEntity)
    {
        var productInfoArray = await msContext.FetchAssortmentInfoExtended($"id={productEntity.MsId};");
        var productInfo = productInfoArray.FirstOrDefault();

        if (productInfo == null)
        {
            SentrySdk.CaptureMessage($"Product with MS ID {productEntity.MsId} not found");
            throw new ApiException(500, $"Product with MS ID {productEntity.MsId} not found");
        }
    }

    private async Task<List<KotovskayaAssortment>> GetNotImplementedProducts(List<Guid> productIds, Guid categoryId)
    {
        var assortment = await msContext.FetchAssortmentInfoExtended($"quantityMode=positiveOnly;" +
                                                                     $"productFolder=https://api.moysklad.ru/api/remap/1.2/entity/productfolder/{categoryId};");
        if (assortment == null)
        {
            throw new ApiException(500, "Assortment not found");
        }
        var notIncluded = assortment
            .Where(kotovskayaAssortment => kotovskayaAssortment != null 
                                           && productIds.Contains(kotovskayaAssortment.Id!.Value) == false).ToList();
        return notIncluded!;
    }

    private async Task RemoveExcessProducts()
    {

    }
}
