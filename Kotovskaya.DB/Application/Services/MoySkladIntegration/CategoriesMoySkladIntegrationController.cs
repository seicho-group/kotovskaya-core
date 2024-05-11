using Confiti.MoySklad.Remap.Api;
using Kotovskaya.DB.Application.Services.Interfaces;
using Kotovskaya.DB.Domain.Context;

namespace Kotovskaya.DB.Application.Services.MoySkladIntegration;

public class CategoriesMoySkladIntegrationController : IIntegrationController<MoySkladApi, KotovskayaDbContext>
{
    private MoySkladApi? Api { get; set; }
    private KotovskayaDbContext? DbContext { get; set; }
    public async Task Migrate(MoySkladApi msApi, KotovskayaDbContext dbContext)
    {
        Api = msApi;
        await MigrateTopCategories();
    }

    private async Task MigrateTopCategories()
    {
        if (Api != null)
        {
            var categoriesResponse = await Api.ProductFolder.GetAllAsync();
        }
        
    }
}