using Confiti.MoySklad.Remap.Api;
using Kotovskaya.DB.Application.Services.Interfaces;

namespace Kotovskaya.DB.Application.Services.MoySkladIntegration;

public class CategoriesMoySkladIntegrationController: IIntegrationController<MoySkladApi>
{
    public Task Migrate(MoySkladApi api)
    {
        throw new NotImplementedException();
    }
}