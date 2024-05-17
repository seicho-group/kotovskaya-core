namespace Kotovskaya.DB.Application.Services.Interfaces;

public interface IMigrator<TIn, TOut>
{
    protected TIn Api { get; set; }
    protected TOut OutApi { get; set; }
    public Task Migrate(List<IMigrationController<TIn, TOut>> controllers);
}