using Confiti.MoySklad.Remap.Entities;
using Kotovskaya.DB.Domain.Context;
using Kotovskaya.DB.Domain.Entities.DatabaseEntities;
using Kotovskaya.DB.Domain.Entities.MoySkladExtensions;
using Microsoft.EntityFrameworkCore;

namespace Kotovskaya.MoySkladUpdater.Application.MigrationServices;

public class Creator(KotovskayaDbContext dbContext)
{
    public async Task CreateProductOrUpdateCategory(KotovskayaAssortment product, Category category, List<ProductEntity> categoryProducts)
    {
        try
        {
            var currentProductEntity = categoryProducts
                .FirstOrDefault(pr => pr.MsId == product.Id);

            if (currentProductEntity != null)
            {
                currentProductEntity.Categories.Add(category);
                await dbContext.SaveChangesAsync();
                return;
            }

            var desc = product.Description;
            var productEntity = new ProductEntity
            {
                Categories = [category],
                MsId = (Guid)product.Id!,
                Name = product.Name,
                Description = desc[..(desc.Length > 2040 ? 2040 : desc.Length)],
                Quantity = (int)(product.Quantity ?? 0),
                Article = null
            };
            var salePrice = new SaleTypes()
            {
                Product = productEntity,
                ProductId = productEntity.Id,
                Price = (int)product.SalePrices[0].Value!,
                OldPrice = (int)product.SalePrices[2].Value!,
            };
            await dbContext.Products.AddAsync(productEntity);
            await dbContext.SaleTypes.AddAsync(salePrice);
            await dbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            SentrySdk.CaptureException(e);
        }
    }

    public async Task CreateCategories(ProductFolder[] categories)
    {
        try
        {
            var newCategories = categories.ToList()
                .Select(category =>
                    new Category()
                    {
                        Id = Guid.NewGuid(),
                        Name = category.Name,
                        MsId = (Guid)category.Id!,
                        Type = CategoryType.Soapmaking
                    });

            await dbContext.Categories.AddRangeAsync(newCategories);
            await dbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            SentrySdk.CaptureException(e);
        }
    }
}
