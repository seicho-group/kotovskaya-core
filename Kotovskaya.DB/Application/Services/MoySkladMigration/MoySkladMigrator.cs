using Confiti.MoySklad.Remap.Api;
using Confiti.MoySklad.Remap.Client;
using Kotovskaya.DB.Application.Services.Interfaces;
using Kotovskaya.DB.Domain.Context;

namespace Kotovskaya.DB.Application.Services.MoySkladMigration;

public class MoySkladMigrator : IMigrator<MoySkladApi, KotovskayaDbContext>
{
    public MoySkladApi Api { get; set; }

    public KotovskayaDbContext OutApi { get; set; }

    public MoySkladMigrator(KotovskayaDbContext dbContext)
    {
        OutApi = dbContext;
        Api = new KotovskayaMsContext();
    }
    
    public async Task Migrate(List<IMigrationController<MoySkladApi, KotovskayaDbContext>> controllers)
    {
        foreach (var integrationController in controllers)
        {
            await integrationController.Migrate(Api, OutApi);
        }
    }
}