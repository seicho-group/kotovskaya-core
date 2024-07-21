using System.Diagnostics.CodeAnalysis;
using Confiti.MoySklad.Remap.Entities;
using Kotovskaya.DB.Domain.Context;
using Kotovskaya.DB.Domain.Entities.DatabaseEntities;
using Kotovskaya.DB.Domain.Entities.MoySkladExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kotovskaya.Products.Application.Services.CatchProductUpdate;

// todo: split it up
public class CatchProductUpdateHandler(KotovskayaDbContext dbContext, 
    KotovskayaMsContext msContext, 
    KotovskayaYandexObjectStorageContext yaContext): IRequestHandler<CatchProductUpdateRequest>
{
    public async Task Handle(CatchProductUpdateRequest request, CancellationToken cancellationToken)
    {
        SentrySdk.CaptureMessage($"CatchProductUpdate {request.events[0].meta.href}");
        var productId = request.events[0].meta.href.Split("/").Last();
        var product = await dbContext.Products
            .Where(pr => pr.MsId == Guid.Parse(productId))
            .FirstOrDefaultAsync(cancellationToken);
        
        var updatedProductsFromMs = await msContext.FetchAssortmentInfoExtended($"id={productId};");
        var updatedProduct = updatedProductsFromMs.FirstOrDefault();
        if (updatedProduct != null)
        {
            var desc = updatedProduct.Description[
                ..(updatedProduct.Description.Length > 2040 
                    ? 2040 
                    : updatedProduct.Description.Length)];
            if (product == null)
            {
                product = await CreateProduct(updatedProduct, cancellationToken);
            }

            await AddCategoriesToProduct(product, updatedProduct);
            product.Name = updatedProduct.Name;
            product.Description = desc;
            product.Quantity = (int)updatedProduct.Quantity!;
            try
            {
                var image = await msContext.FetchProductImage(updatedProduct.Images.Rows[0].Meta.DownloadHref);
                if (image != null)
                {
                    await yaContext.ObjectService.PutAsync(image, $"{updatedProduct.Id}/0.jpg");
                }
                product.ImageLink = $"{updatedProduct.Id}/0";
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                SentrySdk.CaptureException(e);
                SentrySdk.CaptureMessage($"Failed to load image to {product.Id}");
            }
            
            
            var priceType =  await dbContext.SaleTypes.Where(price => price.ProductId == product.Id).FirstOrDefaultAsync();
            if (priceType != null)
            {
                var price = updatedProduct.SalePrices.FirstOrDefault();
                if (price != null)
                {
                    priceType.Price = (int)price.Value!;
                }
                dbContext.SaleTypes.Update(priceType);
            }
            dbContext.Products.Update(product);
        }
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    private async Task<ProductEntity> CreateProduct( KotovskayaAssortment updatedProduct,
        CancellationToken cancellationToken)
    {
        var desc = updatedProduct.Description[
            ..(updatedProduct.Description.Length > 2040
                ? 2040
                : updatedProduct.Description.Length)];

        SentrySdk.CaptureMessage("Product not found after update");
        var product = new ProductEntity
        {
            Name = updatedProduct.Name,
            Description = desc,
            MsId = (Guid)updatedProduct.Id!,
            Quantity = (int)updatedProduct.Quantity!,
            Categories = new List<Category>(),
            ImageLink = null,
            LastUpdatedAt = null
        };
        var saleType = new SaleTypes
        {
            ProductId = product.Id,
            Product = product,
            Price = (int)updatedProduct.SalePrices.FirstOrDefault()?.Value!,
            OldPrice = (int?)updatedProduct.SalePrices.LastOrDefault()?.Value
        };

        product.SaleTypes = saleType;
        dbContext.Products.Add(product);
        await dbContext.SaveChangesAsync(cancellationToken);
        return product;
    }

    private async Task AddCategoriesToProduct(ProductEntity product, KotovskayaAssortment
        updatedProduct)
    {
        var categories = updatedProduct.PathName.Split("/");
        var productCategories = categories.ToArray();
        foreach (var category in categories)
        {
            var categoryEntity = await dbContext.Categories
                .Where(c => c.Name == category)
                .FirstOrDefaultAsync();
            if (categoryEntity == null)
            {
                categoryEntity = new Category
                {
                    Name = category
                };
                dbContext.Categories.Add(categoryEntity);
                await dbContext.SaveChangesAsync();
            }

            if (productCategories.Any(cat => category == cat) == false)
            {
                product.Categories.Add(categoryEntity);
            }
            await dbContext.SaveChangesAsync();
        }
    }
}
