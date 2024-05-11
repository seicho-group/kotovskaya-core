namespace Kotovskaya.DB.Application.Services.Interfaces;

public interface IIntegrationController<TIn, TOut>
{
    public Task Migrate(TIn api, TOut apiTo);
}