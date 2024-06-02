using Confiti.MoySklad.Remap.Entities;
using Kotovskaya.DB.Domain.Context;
using Kotovskaya.DB.Domain.Entities.DatabaseEntities;
using Microsoft.EntityFrameworkCore;

namespace Kotovskaya.DB.Application.Services.UpdatingDataController.cs;

public class UpdatingDataController(KotovskayaMsContext msContext, KotovskayaDbContext dbContext)
{
    public async Task UpdateProductData(List<ProductEntity> products)
    {
        foreach (var product in products.Where(product => product.LastUpdatedAt == null || !((DateTime.Now - product.LastUpdatedAt).Value.TotalDays <= 1)))
        {
            product.LastUpdatedAt = DateTime.Now.ToUniversalTime();

            try
            {
                var productAssortmentFromMoySklad = await msContext.FetchAssortmentInfoExtended($"id={product.MsId};");
                var productFromMoySklad = await msContext.FetchProductInfoExtended(product.MsId);
                await SaveDataAsync(product, productFromMoySklad, productAssortmentFromMoySklad[0]);
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
