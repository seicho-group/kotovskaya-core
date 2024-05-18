using Confiti.MoySklad.Remap.Client;
using Confiti.MoySklad.Remap.Entities;
using Confiti.MoySklad.Remap.Models;
using Kotovskaya.DB.Domain.Context;
using Kotovskaya.DB.Domain.Entities.DatabaseEntities;
using Kotovskaya.TelegramApi.KotovskayaBotController;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kotovskaya.Order.Application.Services.CreateOrder;

public class CreateOrderHandler(KotovskayaDbContext dbContext, KotovskayaMsContext msContext)
    : IRequestHandler<CreateOrderRequest, string>
{
    public async Task<string> Handle(CreateOrderRequest request, CancellationToken cancellationToken)
    {
        var msId = await SaveOrderOnMs(request, cancellationToken);

        if (msId == null && msId?.Payload.Id == null)
            throw new ApiException(500, "Couldn't create order in MS for some reason");

        var orderDbEntity = new DB.Domain.Entities.DatabaseEntities.Order
        {
            MoySkladNumber = msId.Payload.Name,
            AuthorName = request.AuthorName,
            AuthorEmail = request.AuthorMail,
            AuthorPhone = request.AuthorPhone
        };

        foreach (var position in request.Positions)
        {
            var product = await dbContext.Products
                .Include(productEntity => productEntity.SaleTypes)
                .FirstOrDefaultAsync(pr => pr.Id == position.ProductId.ToString(), cancellationToken);

            // Shouldn't go here
            if (product == null)
                throw new ApiException(404, $"Product: {product?.Id} not found, but MS order created");

            if (msId.Payload.Id != null && product.MsId != null)
                await msContext.CreateOrderPositionByOrderId(msId.Payload.Id.Value, product.MsId.Value,
                    position.Quantity,
                    product.SaleTypes?.Price ?? 0);

            await SaveOrderPosition(orderDbEntity, product, cancellationToken);
        }

        await new KotovskayaBotController(Environment.GetEnvironmentVariable("TG_TOKEN")!)
            .SendMessageToChat($"Заказ создан, номер заказа: {msId.Payload.Name} \n \n" +
                               $"Ссылка: https://online.moysklad.ru/app/#customerorder/edit?id={msId.Payload.Id};\n \n" +
                               $"ID в бд: {msId.Payload.Id}\n \n" +
                               $"Позиции: \n {string.Join("\n", request.Positions.Select(pos => $"* {pos.ProductId} : {pos.Quantity}"))}");

        await dbContext.AddAsync(orderDbEntity, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return orderDbEntity.MoySkladNumber;
    }

    private async Task SaveOrderPosition(DB.Domain.Entities.DatabaseEntities.Order parentOrder, ProductEntity product,
        CancellationToken cancellationToken)
    {
        var orderPosition = new OrderPosition
        {
            OrderId = parentOrder.Id,
            Order = parentOrder,
            Product = product,
            ProductId = product.Id
        };
        await dbContext.AddAsync(orderPosition, cancellationToken);
    }

    private async Task<ApiResponse<CustomerOrder>?> SaveOrderOnMs(CreateOrderRequest request,
        CancellationToken cancellationToken)
    {
        var organizationId = Environment.GetEnvironmentVariable("MS_ORG_ID");
        if (organizationId == null)
            throw new ApiException(500, "No organization id, call developers");

        var organization = await msContext.Organization.GetAsync(Guid.Parse(organizationId));

        var counterAgentId = Environment.GetEnvironmentVariable("MS_COUNTERAGENT_ID");
        if (counterAgentId == null)
            throw new ApiException(500, "No counteragent id, call developers");

        var agent = await msContext.Counterparty.GetAsync(Guid.Parse(counterAgentId));

        var description =
            $"Имя: {request.AuthorName}\n" +
            $"Почта: {request.AuthorMail}\n " +
            $"Телефон:{request.AuthorPhone}\n " +
            $"Способ доставки: {request.DeliveryWay}\n" +
            $"Комментарий: {request.Comment}";

        var moySkladRequest = new CustomerOrder
        {
            Organization = organization.Payload,
            Agent = agent.Payload,
            Description = description
        };

        return await msContext.CustomerOrder.CreateAsync(moySkladRequest);
    }
}
