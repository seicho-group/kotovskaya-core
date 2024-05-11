using Microsoft.EntityFrameworkCore;

namespace Kotovskaya.DB.Application.Services.Interfaces;

public interface IIntegration<T>
{
    protected T Api { get; set; }
    public Task Migrate(List<IIntegrationController<T>> controllers);
}