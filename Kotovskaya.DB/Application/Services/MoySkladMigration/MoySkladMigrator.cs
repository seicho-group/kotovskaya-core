using Kotovskaya.DB.Application.Services.Interfaces;
using Kotovskaya.DB.Domain.Context;

namespace Kotovskaya.DB.Application.Services.MoySkladMigration;

public class MoySkladMigrator(KotovskayaDbContext dbContext, KotovskayaMsContext kotovskayaMsContext)
    : IMigrator<KotovskayaMsContext, KotovskayaDbContext>
{
    public KotovskayaMsContext Api { get; set; } = kotovskayaMsContext;
    public KotovskayaDbContext OutApi { get; set; } = dbContext;

    public async Task Migrate(List<IMigrationController<KotovskayaMsContext, KotovskayaDbContext>> controllers)
    {
        foreach (var integrationController in controllers) await integrationController.Migrate(Api, OutApi);
    }
}