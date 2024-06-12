using Confiti.MoySklad.Remap.Client;
using Kotovskaya.DB.Domain.Context;
using Kotovskaya.Shared.Application.Entities.DTO;
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
        if (product == null)
        {
            SentrySdk.CaptureMessage("Product not found after update");
            return;
        }
        var updatedProductsFromMs = await msContext.FetchAssortmentInfoExtended($"id={productId};");
        var updatedProduct = updatedProductsFromMs.FirstOrDefault();
        if (updatedProduct != null)
        {
            product.Name = updatedProduct.Name;
            product.Description = updatedProduct.Description[..(updatedProduct.Description.Length > 2040 ? 2040 : updatedProduct.Description.Length)];
            product.Quantity = (int)updatedProduct.Quantity!;
            try
            {
                var image = await msContext.FetchProductImage(updatedProduct.Images.Rows[0].Meta.DownloadHref);
                if (image != null)
                {
                    await yaContext.ObjectService.PutAsync(image, $"{updatedProduct.Id}/0.jpg");
                }
                product.ImageLink = $"{updatedProduct.Id}/0.jpg";
            }
            catch (Exception e)
            {
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
}