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
        var msCategories = await msContext.FetchProductFoldersExtended();

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

            await MigrateCategoriesByPathName(newDbCategories, "", null);
        }
    }
    
    private async Task MigrateCategoriesByPathName(ProductFolder[] categories, string pathName, Category? parentFolder)
    {
        var childCategories = categories
            .Where(c => c.PathName.Split("/").Last() == pathName);

        foreach (var childCategory in childCategories.ToList())
        {
            var existingCategory = await dbContext.Categories
                .FirstOrDefaultAsync(cat => cat.Id == childCategory.Id);

            var newCategoryModel = new Category
            {
                ParentCategory = parentFolder,
                ParentCategoryId = parentFolder?.Id,
                Id = existingCategory?.Id ?? childCategory.Id ?? Guid.NewGuid(),
                Name = childCategory.Name,
                Type = existingCategory?.Type ?? CategoryType.Soapmaking,
                IsVisible = existingCategory?.IsVisible ?? true,
                MsId = (Guid)childCategory.Id!
            };

            if (existingCategory != null) dbContext.Categories.Remove(existingCategory);
            await dbContext.Categories.AddAsync(newCategoryModel);
            await dbContext.SaveChangesAsync();
            await MigrateCategoriesByPathName(categories, childCategory.Name, newCategoryModel);
        }
    }
}
