using Kotovskaya.DB.Domain.Context;
using Microsoft.EntityFrameworkCore;

namespace Kotovskaya.DB.Application.Services;

public class RemoveProductDublicatesController
{
    public async Task Remove(KotovskayaDbContext dbContext)
    {
        var products = await dbContext.Products
            .GroupBy(p => p.MsId)
            .ToListAsync();

        var productsToDelete = products
            .Where(g => g.Key != null)
            .SelectMany(g => g.Skip(1).ToList()).ToList();

        dbContext.Products.RemoveRange(productsToDelete);
        await dbContext.SaveChangesAsync();
    }
}
