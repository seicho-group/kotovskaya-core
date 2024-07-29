using Confiti.MoySklad.Remap.Client;
using Confiti.MoySklad.Remap.Entities;
using Confiti.MoySklad.Remap.Models;
using Kotovskaya.DB.Domain.Context;
using Kotovskaya.DB.Domain.Entities.Enum;
using Kotovskaya.Order.Domain.PositionService;
using Kotovskaya.TelegramApi.KotovskayaBotController;
using MediatR;

namespace Kotovskaya.Order.Application.Services.CreateOrder;

// todo: ОТРЕФАЧИТЬ И ВЫНЕСТИ ВСЕ КУДА-НИБУДЬ + навесить сюда кролика
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

        var positionsService = new PositionService(dbContext, msContext);
        var positionsList = await positionsService.SavePositionsOnMoySklad(request.Positions, orderDbEntity, msId.Payload.Id);


        // убрать в кролика
        await new KotovskayaBotController(Environment.GetEnvironmentVariable("TG_TOKEN")!)
            .SendMessageToChat($"Заказ создан, номер заказа: {msId.Payload.Name} \n \n" +
                               $"Ссылка: https://online.moysklad.ru/app/#customerorder/edit?id={msId.Payload.Id};\n \n" +
                               $"ID в бд: {msId.Payload.Id}\n \n" +
                               $"Позиции: \n {string.Join("\n", request.Positions.Select(pos => $"* {pos.ProductId} : {pos.Quantity}"))}");

        await dbContext.AddAsync(orderDbEntity, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return orderDbEntity.MoySkladNumber;
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
            $"Способ доставки: {request.DeliveryWay.GetDeliveryWayName()}\n" +
            $"Комментарий: {request.Comment}";

        var moySkladRequest = new CustomerOrder
        {
            Organization = organization.Payload,
            Agent = agent.Payload,
            Description = description
        };
        try
        {
            return await msContext.CustomerOrder.CreateAsync(moySkladRequest);
        }
        catch (Exception e)
        {
            SentrySdk.CaptureMessage("Error happened while creating order on MS");
            throw new ApiException(500, "Couldn't create order in MS reason");
        }
    }
}
