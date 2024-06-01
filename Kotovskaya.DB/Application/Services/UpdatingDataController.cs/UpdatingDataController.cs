using Confiti.MoySklad.Remap.Entities;
using Kotovskaya.DB.Domain.Context;
using Kotovskaya.DB.Domain.Entities.DatabaseEntities;
using Microsoft.EntityFrameworkCore;

namespace Kotovskaya.DB.Application.Services.UpdatingDataController.cs;

public class UpdatingDataController(KotovskayaMsContext msContext, KotovskayaDbContext dbContext)
{
    public async Task UpdateProductData(List<ProductEntity> products)
    {
        foreach (var product in products)
        {
            if (product.MsId == null)
            {
                SentrySdk.CaptureMessage($"Product: {product.Id} has no MoySklad ID");
                continue;
            }

            // if updated before last night, updating info
            if (product.LastUpdatedAt != null && (DateTime.Now - product.LastUpdatedAt).Value.TotalDays <= 1)
                continue;

            product.LastUpdatedAt = DateTime.Now.ToUniversalTime();

            try
            {
                var productAssortmentFromMoySklad = await msContext.FetchAssortmentInfoExtended(product.MsId.Value);
                var productFromMoySklad = await msContext.FetchProductInfoExtended(product.MsId.Value);
                await SaveDataAsync(product, productFromMoySklad, productAssortmentFromMoySklad);
            }
            catch
            {
                SentrySdk.CaptureMessage($"Product or assortment with ID: {product.Id} is null");
            }
        }
    }

    private async Task SaveDataAsync(ProductEntity productEntity, Product? product, Assortment? assortment)
    {
        if (product == null || assortment == null)
        {
            throw new ApplicationException("Not found");
        }

        productEntity.Quantity = assortment?.Quantity != null ?
            (int)assortment.Quantity.Value
            : 0;

        var newSalePrice = new SaleTypes()
        {
            Product = productEntity,
            OldPrice = (int)(product.SalePrices[2].Value ?? 0),
            Price = (int)(product.SalePrices[0].Value ?? 0)
        };

        var oldSalePrice = await dbContext.SaleTypes.Where(st => productEntity.Id == st.Product.Id).FirstOrDefaultAsync();
        if (oldSalePrice != null) dbContext.SaleTypes.Remove(oldSalePrice);
        productEntity.SaleTypes = newSalePrice;
        await dbContext.SaveChangesAsync();
    }
}
