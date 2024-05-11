using Confiti.MoySklad.Remap.Api;
using Confiti.MoySklad.Remap.Entities;
using Kotovskaya.DB.Application.Services.Interfaces;
using Kotovskaya.DB.Domain.Context;
using Kotovskaya.DB.Domain.Interfaces;

namespace Kotovskaya.DB.Application.Services.MoySkladIntegration;

public class CategoriesMoySkladIntegrationController : IIntegrationController<MoySkladApi, KotovskayaDbContext>
{
    private MoySkladApi? Api { get; set; }
    private KotovskayaDbContext? KotovskayaDbContext { get; set; }
    public async Task Migrate(MoySkladApi msApi, KotovskayaDbContext dbContext)
    {
        Api = msApi;
        KotovskayaDbContext = dbContext;
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
                var childCategoryModel = new Category
                {
                    ParentCategory = parentFolder, 
                    Id = childCategory.Id.ToString() ?? Guid.NewGuid().ToString(), 
                    Name = childCategory.Name,
                    Type = CategoryType.Soapmaking,
                    IsVisible = true,
                    MSId = childCategory.Id.ToString()
                };
                KotovskayaDbContext.Categories.Add(childCategoryModel);

                MigrateCategoriesByPathName(categories, childCategory.Name, childCategoryModel);
            });
        }
    }
}