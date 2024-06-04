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
                .ToArray();

            await new Creator(dbContext).CreateCategories(newDbCategories);
        }
    }
}
