using Kotovskaya.DB.Application.Services.Interfaces;
using Kotovskaya.DB.Domain.Context;
using Microsoft.EntityFrameworkCore;

namespace Kotovskaya.DB.Application.Services.MoySkladMigration;

public class ImagesMoySkladMigrationController(KotovskayaYandexObjectStorageContext yandexObjectStorageContext): IMigrationController<KotovskayaMsContext, KotovskayaDbContext>
{
    public async Task Migrate(KotovskayaMsContext msContext, KotovskayaDbContext dbContext)
    {
        var productEntities = await dbContext.Products.ToListAsync();
        foreach (var productEntity in productEntities)
        {
            if (productEntity?.MsId == null) continue;
            var productImages = await msContext.FetchProductImagesExtended(productEntity.MsId.Value);

            if (productImages?.Rows == null || productImages.Rows.Length == 0) continue;
            var productImage = await msContext.FetchProductImage(productImages.Rows[0].Meta.DownloadHref);

            if (productImage == null) continue;
            await yandexObjectStorageContext.ObjectService.PutAsync(productImage, $"{productEntity.Id}.jpg");

            productEntity.ImageLink = $"{productEntity.Id}.jpg";
            await dbContext.SaveChangesAsync();
        }
    }
}
