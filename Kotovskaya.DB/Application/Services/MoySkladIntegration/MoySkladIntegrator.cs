using Confiti.MoySklad.Remap.Api;
using Confiti.MoySklad.Remap.Client;
using Kotovskaya.DB.Application.Services.Interfaces;
using Kotovskaya.DB.Domain.Context;

namespace Kotovskaya.DB.Application.Services.MoySkladIntegration;

public class MoySkladIntegrator : IIntegrator<MoySkladApi, KotovskayaDbContext>
{
    public MoySkladApi Api { get; set; }

    public KotovskayaDbContext OutApi { get; set; }

    public MoySkladIntegrator(KotovskayaDbContext dbContext)
    {
        OutApi = dbContext;
        
        var credentials = new MoySkladCredentials()
        {
            AccessToken = Environment.GetEnvironmentVariable("MS_TOKEN")
        };
        Api = new MoySkladApi(credentials);
    }
    
    public async Task Migrate(List<IIntegrationController<MoySkladApi, KotovskayaDbContext>> controllers)
    {
        foreach (var integrationController in controllers)
        {
            await integrationController.Migrate(Api, OutApi);
        }
    }
}