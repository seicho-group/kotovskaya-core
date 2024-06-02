using Confiti.MoySklad.Remap.Client;
using Kotovskaya.DB.Domain.Context;
using Kotovskaya.DB.Domain.Entities.DatabaseEntities;
using Kotovskaya.Shared.Application.Entities.DTO;
using Microsoft.EntityFrameworkCore;

namespace Kotovskaya.Order.Domain.PositionService;

public class PositionService(KotovskayaDbContext dbContext, KotovskayaMsContext msContext)
{
    public async Task<List<ProductEntity>> SavePositionsOnMoySklad(List<OrderPositionDto> positions, 
        DB.Domain.Entities.DatabaseEntities.Order orderDbEntity, 
        Guid? moySkladOrderId)
    {
        var positionsList = new List<ProductEntity>();
        foreach (var position in positions)
        {
            var product = await dbContext.Products
                .Include(productEntity => productEntity.SaleTypes)
                .FirstOrDefaultAsync(pr => pr.Id == position.ProductId.ToString());

            // Shouldn't go here
            if (product == null)
                throw new ApiException(404, $"Product: {product?.Id} not found, but MS order created");

            positionsList.Add(product);
            if (moySkladOrderId == null || product.MsId == null) continue;

            try
            {
                await msContext.CreateOrderPositionByOrderId(moySkladOrderId.Value, product.MsId.Value,
                    position.Quantity,
                    product.SaleTypes?.Price ?? 0);
            }
            catch
            {
                SentrySdk.CaptureMessage($"Order position {moySkladOrderId}:{product.MsId} not created in moysklad");
            }
        }

        return positionsList;
    }
    
    public async Task SaveOrderPosition(DB.Domain.Entities.DatabaseEntities.Order parentOrder, ProductEntity product)
    {
        var orderPosition = new OrderPosition
        {
            OrderId = parentOrder.Id,
            Order = parentOrder,
            Product = product,
            ProductId = product.Id
        };
        try
        {
            await dbContext.AddAsync(orderPosition);
        }
        catch (Exception e)
        {
            SentrySdk.CaptureMessage($"Order position {parentOrder.Id}:{product.Id} not created");
            throw;
        }

    }
}
