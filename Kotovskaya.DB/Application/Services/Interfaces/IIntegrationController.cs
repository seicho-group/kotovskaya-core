namespace Kotovskaya.DB.Application.Services.Interfaces;

public interface IIntegrationController<I, O>
{
    public Task Migrate(I Api, O ApiTo);
}