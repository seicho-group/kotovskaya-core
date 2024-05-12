using AutoMapper;
using Kotovskaya.DB.Domain.Context;
using MediatR;

namespace Kotovskaya.Order.Application.Services.CreateOrder;

public class CreateOrderHandler(KotovskayaDbContext dbContext, KotovskayaMsContext msContext, IMapper mapper) 
    : IRequestHandler<CreateOrderRequest, string>
{
    public async Task<string> Handle(CreateOrderRequest request, CancellationToken cancellationToken)
    {
        var orderDbEntity = new DB.Domain.Entities.Order()
        {
            MoySkladNumber = "123",
            AuthorName = request.AuthorName,
            AuthorEmail = request.AuthorMail,
            AuthorPhone = request.AuthorPhone
        };

        dbContext.Add(orderDbEntity);
        await dbContext.SaveChangesAsync(cancellationToken); 
        
        return orderDbEntity.MoySkladNumber;
    }
}