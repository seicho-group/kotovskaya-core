using Confiti.MoySklad.Remap.Client;
using Kotovskaya.DB.Domain.Context;
using Kotovskaya.DB.Domain.Entities.DatabaseEntities;
using Kotovskaya.DB.Domain.Entities.MoySkladExtensions;
using Kotovskaya.MoySkladUpdater.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Kotovskaya.MoySkladUpdater.Application.MigrationServices;

public class ProductUpdater(KotovskayaMsContext msContext, KotovskayaDbContext dbContext) : IMigrationService
{
    public async Task Migrate()
    {
        var products = await dbContext.Products
            .Include(pr => pr.SaleTypes)
            .ToListAsync();

        // Taking not included in db products
        var notImplemented = await GetNotImplementedProducts(products.Select(pr => pr.MsId).ToList());
        Console.WriteLine(products);
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

    private async Task<List<KotovskayaAssortment>> GetNotImplementedProducts(List<Guid> productIds)
    {
        var assortment = await msContext.FetchAssortmentInfoExtended("quantityMode=positiveOnly;");
        if (assortment == null)
        {
            throw new ApiException(500, "Assortment not found");
        }
        return assortment.Where(kotovskayaAssortment => kotovskayaAssortment != null && !productIds.Contains((Guid)kotovskayaAssortment.Id)).ToList();
    }

    private async Task RemoveExcessProducts()
    {

    }
}
