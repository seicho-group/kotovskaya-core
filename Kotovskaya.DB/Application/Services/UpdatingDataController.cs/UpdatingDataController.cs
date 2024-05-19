using Kotovskaya.DB.Domain.Context;
using Microsoft.EntityFrameworkCore;

namespace Kotovskaya.DB.Application.Services.UpdatingDataController.cs;

public class UpdatingDataController(KotovskayaMsContext msContext, KotovskayaDbContext dbContext)
{
    public async Task UpdateProductData(List<string> productIds)
    {
        foreach (var productId in productIds)
        {
            var product = await dbContext.Products.Where(pr => pr.Id == productId).FirstOrDefaultAsync();
            if (product?.MsId == null) continue;

            // if updated after last night, updating info
            if (product.LastUpdatedAt > DateTime.Now.AddDays(-1))
            {
                continue;
            }

            var productFromMoySklad = await msContext.FetchAssortmentInfoExtended(product.MsId.Value);
            if (productFromMoySklad == null) continue;

            product.Quantity = productFromMoySklad.Quantity != null ? (int)productFromMoySklad.Quantity.Value : 0;

            await dbContext.SaveChangesAsync();
        }
    }
}
