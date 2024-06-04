using Confiti.MoySklad.Remap.Entities;
using Kotovskaya.DB.Domain.Context;
using Kotovskaya.DB.Domain.Entities.DatabaseEntities;
using Kotovskaya.DB.Domain.Entities.MoySkladExtensions;
using Microsoft.EntityFrameworkCore;

namespace Kotovskaya.MoySkladUpdater.Application.MigrationServices;

public class Creator(KotovskayaDbContext dbContext, KotovskayaMsContext msContext, KotovskayaYandexObjectStorageContext yandexObjectStorageContext)
{
    public async Task CreateProductOrUpdateCategory(KotovskayaAssortment product, Category category, List<ProductEntity> categoryProducts)
    {
        try
        {
            var currentProductEntity = await dbContext.Products
                .FirstOrDefaultAsync(pr => pr.MsId == product.Id);

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

    public async Task CreateProductImage(ProductEntity productEntity)
    {
        if (productEntity?.MsId == null) return;
        var productImages = await msContext.FetchProductImagesExtended(productEntity.MsId);

        if (productImages?.Rows == null || productImages.Rows.Length == 0) return;
        var productImage = await msContext.FetchProductImage(productImages.Rows[0].Meta.DownloadHref);

        if (productImage == null)
        {
            SentrySdk.CaptureMessage($"No image present for product: {productEntity.Id}:{productEntity.Name}");
            return;
        }
        await yandexObjectStorageContext.ObjectService.PutAsync(productImage, $"{productEntity.Id}/0.jpg");

        productEntity.ImageLink = $"{productEntity.Id}.jpg";
        await dbContext.SaveChangesAsync();
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
