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
            if (product?.MsId == null) continue;

            // if updated before last night, updating info
            if (product.LastUpdatedAt != null && (DateTime.Now - product.LastUpdatedAt).Value.TotalDays <= 1)
            {
                continue;
            }
            product.LastUpdatedAt = DateTime.Now.ToUniversalTime();

            var productAssortmentFromMoySklad = await msContext.FetchAssortmentInfoExtended(product.MsId.Value);
            var productFromMoySklad = await msContext.FetchProductInfoExtended(product.MsId.Value);
            if (productFromMoySklad == null) continue;

            product.Quantity = productAssortmentFromMoySklad?.Quantity != null ?
                (int)productAssortmentFromMoySklad.Quantity.Value
                : 0;

            var newSalePrice = new SaleTypes()
            {
                Product = product,
                OldPrice = (int)(productFromMoySklad.SalePrices[2].Value ?? 0),
                Price = (int)(productFromMoySklad.SalePrices[0].Value ?? 0)
            };

            var oldSalePrice = await dbContext.SaleTypes.Where(st => product.Id == st.Product.Id).FirstOrDefaultAsync();
            if (oldSalePrice != null) dbContext.SaleTypes.Remove(oldSalePrice);
            product.SaleTypes = newSalePrice;
            await dbContext.SaveChangesAsync();
        }
    }
}
