using Kotovskaya.DB.Application.Services.Interfaces;
using Kotovskaya.DB.Domain.Context;
using Kotovskaya.DB.Domain.Entities.DatabaseEntities;
using Microsoft.EntityFrameworkCore;

namespace Kotovskaya.DB.Application.Services.MoySkladMigration;

public class SaleTypesMoySkladMigrationController : IMigrationController<KotovskayaMsContext, KotovskayaDbContext>
{
    public async Task Migrate(KotovskayaMsContext api, KotovskayaDbContext dbContext)
    {
        var products = await dbContext.Products
            .Include(productEntity => productEntity.SaleTypes)
            .ToListAsync();

        foreach (var product in products.Where(product => product.SaleTypes == null))
        {
            // id no msid we cant get prices
            if (product.MsId == null) continue;

            var productFromMs = await api.FetchProductInfoExtended(product.MsId.Value);
            // if we got no sale prices we cant fill them in db
            if (productFromMs?.SalePrices == null) continue;

            var saleType = new SaleTypes
            {
                Product = product,
                OldPrice = (int)(productFromMs.SalePrices[2].Value ?? 0),
                Price = (int)(productFromMs.SalePrices[0].Value ?? 0)
            };

            product.SaleTypes = saleType;
            await dbContext.SaveChangesAsync();
        }
    }
}
