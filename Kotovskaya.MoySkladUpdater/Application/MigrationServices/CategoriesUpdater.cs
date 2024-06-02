using Confiti.MoySklad.Remap.Entities;
using Kotovskaya.DB.Domain.Context;
using Kotovskaya.DB.Domain.Entities.DatabaseEntities;
using Kotovskaya.MoySkladUpdater.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Kotovskaya.MoySkladUpdater.Application.MigrationServices;

public class CategoriesUpdater(KotovskayaMsContext msContext, KotovskayaDbContext dbContext) : IMigrationService
{
    public async Task Migrate()
    {
        var msCategoriesResponse = await msContext.ProductFolder.GetAllAsync();
        var msCategories = msCategoriesResponse.Payload.Rows;

        var dbCategories = await dbContext.Categories.ToListAsync();

        await AddNotImplemented(dbCategories, msCategories);
    }

    private async Task AddNotImplemented(List<Category> dbCategories, ProductFolder[]? msCategories)
    {

        var dbCategoriesIds = dbCategories.Select(row => row.MsId).ToList();

        if (msCategories != null)
        {
            var newDbCategories = msCategories
                .Where(msCategory => !dbCategoriesIds.Contains((Guid)msCategory.Id!))
                .Select(msCategory => new Category()
                {
                    Id = Guid.NewGuid(),
                    Name = msCategory.Name,
                    MsId = (Guid)msCategory.Id!,
                    Type = CategoryType.Soapmaking
                })
                .ToList();

            await dbContext.AddRangeAsync(newDbCategories);
        }

        await dbContext.SaveChangesAsync();
    }
}
