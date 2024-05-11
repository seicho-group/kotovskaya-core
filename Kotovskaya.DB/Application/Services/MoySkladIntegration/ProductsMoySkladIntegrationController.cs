using Confiti.MoySklad.Remap.Api;
using Kotovskaya.DB.Application.Services.Interfaces;
using Kotovskaya.DB.Domain.Context;

namespace Kotovskaya.DB.Application.Services.MoySkladIntegration;

public class ProductsMoySkladIntegrationController: IIntegrationController<MoySkladApi, KotovskayaDbContext>
{
    public async Task Migrate(MoySkladApi api, KotovskayaDbContext dbContext)
    {
        await Task.Delay(21);
        Console.WriteLine("123123");
    }
}