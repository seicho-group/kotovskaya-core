namespace Kotovskaya.DB.Application.Services.Interfaces;

public interface IIntegrationController<T>
{
    public Task Migrate(T Api);
}