using AutoMapper;
using Kotovskaya.DB.Domain.Context;
using Kotovskaya.DB.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kotovskaya.Order.Application.Services.CreateOrder;

public class CreateOrderHandler(KotovskayaDbContext dbContext, KotovskayaMsContext msContext) 
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
        var product = await dbContext.Products
            .FirstOrDefaultAsync(pr => pr.Id == "a75b392b-7373-404c-9f8e-5c2a3c2e1459", cancellationToken);
        if (product != null)
        {
            var orderPosition = new OrderPosition()
            {
                OrderId = orderDbEntity.Id,
                Order = orderDbEntity,
                Product = product,
                ProductId = product.Id
            };
            await dbContext.AddAsync(orderPosition, cancellationToken);
        }
        

        await dbContext.AddAsync(orderDbEntity, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken); 
        
        return orderDbEntity.MoySkladNumber;
    }
}