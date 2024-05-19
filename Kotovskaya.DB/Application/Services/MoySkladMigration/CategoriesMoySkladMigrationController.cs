using Confiti.MoySklad.Remap.Api;
using Confiti.MoySklad.Remap.Entities;
using Kotovskaya.DB.Application.Services.Interfaces;
using Kotovskaya.DB.Domain.Context;
using Kotovskaya.DB.Domain.Entities.DatabaseEntities;
using Microsoft.EntityFrameworkCore;

namespace Kotovskaya.DB.Application.Services.MoySkladMigration;

public class CategoriesMoySkladMigrationController : IMigrationController<KotovskayaMsContext, KotovskayaDbContext>
{
    private MoySkladApi? Api { get; set; }
    private KotovskayaDbContext? KotovskayaDbContext { get; set; }

    public async Task Migrate(KotovskayaMsContext api, KotovskayaDbContext apiTo)
    {
        Api = api;
        KotovskayaDbContext = apiTo;
        if (Api != null)
        {
            var categoriesResponse = await Api.ProductFolder.GetAllAsync();
            await MigrateCategoriesByPathName(categoriesResponse.Payload.Rows, "", null);

        }
    }

    private async Task MigrateCategoriesByPathName(ProductFolder[] categories, string pathName, Category? parentFolder)
    {
        var childCategories = categories.Where(c => c.PathName.Split("/").Last() == pathName);

        if (KotovskayaDbContext == null) return;

        foreach (var childCategory in childCategories.ToList())
        {
            var existingCategory = await KotovskayaDbContext.Categories
                .FirstOrDefaultAsync(cat => cat.Id == childCategory.Id.ToString());

            var newCategoryModel = new Category
            {
                ParentCategory = parentFolder,
                ParentCategoryId = parentFolder?.Id,
                Id = existingCategory?.Id ?? childCategory.Id.ToString() ?? Guid.NewGuid().ToString(),
                Name = childCategory.Name,
                Type = existingCategory?.Type ?? CategoryType.Soapmaking,
                IsVisible = existingCategory?.IsVisible ?? true,
                MsId = childCategory.Id.ToString()
            };

            if (existingCategory != null) KotovskayaDbContext.Categories.Remove(existingCategory);
            await KotovskayaDbContext.Categories.AddAsync(newCategoryModel);
            await KotovskayaDbContext.SaveChangesAsync();
            await MigrateCategoriesByPathName(categories, childCategory.Name, newCategoryModel);
        }
    }
}
