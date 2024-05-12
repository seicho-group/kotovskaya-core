namespace Kotovskaya.DB.Application.Services.Interfaces;

public interface IMigrationController<TIn, TOut>
{
    public Task Migrate(TIn api, TOut apiTo);
}