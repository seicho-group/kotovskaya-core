using Confiti.MoySklad.Remap.Api;
using Confiti.MoySklad.Remap.Client;
using Kotovskaya.DB.Application.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Kotovskaya.DB.Application.Services.MoySkladIntegration;

public class MoySkladIntegrations : IIntegration<MoySkladApi>
{
    public MoySkladApi Api { get; set; }

    private DbContext AppDbContext { get; init; }
    public MoySkladIntegrations(DbContext dbContext)
    {

        AppDbContext = dbContext;

    }
    
    public async Task Migrate(List<IIntegrationController<MoySkladApi>> controllers)
    {
        foreach (var integrationController in controllers)
        {
            await integrationController.Migrate(Api);
        }
    }
}