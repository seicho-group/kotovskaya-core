using Kotovskaya.Shared.Application.Entities.MoySkladWebhook;
using MediatR;

namespace Kotovskaya.Products.Application.Services.CatchProductUpdate;

public class CatchProductUpdateRequest : IRequest
{
    public required ProductUpdateEvent[] events { get; set; }
}