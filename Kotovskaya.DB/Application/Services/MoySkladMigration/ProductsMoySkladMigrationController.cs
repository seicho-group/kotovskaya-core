using Confiti.MoySklad.Remap.Api;
using Confiti.MoySklad.Remap.Client;
using Confiti.MoySklad.Remap.Entities;
using Kotovskaya.DB.Application.Services.Interfaces;
using Kotovskaya.DB.Domain.Context;
using Kotovskaya.DB.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kotovskaya.DB.Application.Services.MoySkladMigration;

public class ProductsMoySkladMigrationController: IMigrationController<MoySkladApi, KotovskayaDbContext>
{
    public async Task Migrate(MoySkladApi api, KotovskayaDbContext dbContext)
    {
        var categories = await dbContext.Categories.ToListAsync();
        
        foreach (var category in categories)
        {
            var assortment = await GetAllAssortmentByFolder(api, category);
            var products = assortment.Select(product =>
            {
                var desc = product.Product.Description ?? "";
                return new ProductEntity()
                {
                    Id = Guid.NewGuid().ToString(),
                    Category = category,
                    CategoryId = category.Id,
                    MsId = product.Id,
                    Name = product.Name,
                    Description = desc.Substring(0, desc.Length > 2040 ? 2040 : desc.Length),
                    Quantity = (int)(product.Quantity ?? 0),
                    Article = null,
                };
            });
            dbContext.Products.AddRange(products.ToArray());
        }

        await dbContext.SaveChangesAsync();
    }

    private async Task<Assortment[]> GetAllAssortmentByFolder(MoySkladApi api, Category category)
    {
        if (category.MsId == null)
        {
            return [];
        }
        
        var productFolder = await api.ProductFolder.GetAsync(Guid.Parse(category.MsId));
        
        var query = new AssortmentApiParameterBuilder();
        query.Parameter(p => p.ProductFolder).Should().Be(productFolder.Payload);
        var categoryAssortment = await api.Assortment.GetAllAsync(query);
        
        return categoryAssortment.Payload.Rows;
    }
}