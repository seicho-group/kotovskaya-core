using Confiti.MoySklad.Remap.Entities;

namespace Kotovskaya.DB.Application.Services;

public class ProductCreator
{
    public Task Execute(Assortment product)
    {
        return Task.CompletedTask;
    }
}
