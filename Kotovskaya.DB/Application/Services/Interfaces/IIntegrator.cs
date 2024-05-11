namespace Kotovskaya.DB.Application.Services.Interfaces;

public interface IIntegrator<TIn, TOut>
{
    protected TIn Api { get; set; }
    protected TOut OutApi { get; set; }
    public Task Migrate(List<IIntegrationController<TIn, TOut>> controllers);
}