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
            if (product.LastUpdatedAt < DateTime.Now.AddDays(-1))
            {
                continue;
            }
            product.LastUpdatedAt = DateTime.Now.ToUniversalTime();

            var productFromMoySklad = await msContext.FetchAssortmentInfoExtended(product.MsId.Value);
            if (productFromMoySklad == null) continue;

            product.Quantity = productFromMoySklad.Quantity != null ? (int)productFromMoySklad.Quantity.Value : 0;
        }
        await dbContext.SaveChangesAsync();
    }
}
