using Microsoft.EntityFrameworkCore;

namespace Kotovskaya.DB.Application.Services.Interfaces;

public interface IIntegrator<I, O>
{
    protected I Api { get; set; }
    protected O OutApi { get; set; }
    public Task Migrate(List<IIntegrationController<I, O>> controllers);
}