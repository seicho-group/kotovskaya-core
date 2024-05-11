using Confiti.MoySklad.Remap.Api;
using Confiti.MoySklad.Remap.Entities;
using Kotovskaya.DB.Application.Services.Interfaces;
using Kotovskaya.DB.Domain.Context;
using Kotovskaya.DB.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kotovskaya.DB.Application.Services.MoySkladIntegration;

public class CategoriesMoySkladIntegrationController : IIntegrationController<MoySkladApi, KotovskayaDbContext>
{
    private MoySkladApi? Api { get; set; }
    private KotovskayaDbContext? KotovskayaDbContext { get; set; }
    public async Task Migrate(MoySkladApi api, KotovskayaDbContext apiTo)
    {
        Api = api;
        KotovskayaDbContext = apiTo;
        if (Api != null)
        {
            var categoriesResponse = await Api.ProductFolder.GetAllAsync();
            MigrateCategoriesByPathName(categoriesResponse.Payload.Rows, "", null);
            await KotovskayaDbContext.SaveChangesAsync();
        }
    }

    private void MigrateCategoriesByPathName(ProductFolder[] categories, string pathName, Category? parentFolder)
    {
        var childCategories = categories.Where(c => c.PathName.Split("/")[0] == pathName);

        if (KotovskayaDbContext != null)
        {
            childCategories.ToList().ForEach(childCategory =>
            {
                var existingCategory = KotovskayaDbContext.Categories
                    .FirstOrDefault(cat => cat.Id == childCategory.Id.ToString());
                
                var newCategoryModel = new Category
                {
                    ParentCategory = parentFolder, 
                    Id = childCategory.Id.ToString() ?? Guid.NewGuid().ToString(), 
                    Name = childCategory.Name,
                    Type = existingCategory?.Type ?? CategoryType.Soapmaking,
                    IsVisible = existingCategory?.IsVisible ?? true,
                    MsId = childCategory.Id.ToString()
                };

                if (existingCategory != null)
                {
                    KotovskayaDbContext.Categories.Remove(existingCategory);    
                }
                KotovskayaDbContext.Categories.Add(newCategoryModel);

                MigrateCategoriesByPathName(categories, childCategory.Name, newCategoryModel);
            });
        }
    }
}