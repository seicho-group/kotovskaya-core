using Kotovskaya.DB.Domain.Context;
using Kotovskaya.MoySkladUpdater.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Kotovskaya.MoySkladUpdater.Application.MigrationServices;

public class ImagesUpdater(   
    KotovskayaMsContext msContext, 
    KotovskayaDbContext dbContext, 
    KotovskayaYandexObjectStorageContext yandexObjectStorageContext) : IMigrationService
{
    public async Task Migrate()
    {
        var products = await dbContext.Products
            .Where(pr => pr.ImageLink == null)
            .ToListAsync();

        var creator = new Creator(dbContext, msContext, yandexObjectStorageContext);
        foreach (var pr in products) await creator.CreateProductImage(pr);
    }
}