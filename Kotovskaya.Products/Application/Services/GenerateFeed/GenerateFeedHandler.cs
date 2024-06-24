using Kotovskaya.DB.Domain.Context;
using MediatR;

namespace Kotovskaya.Products.Application.Services.GenerateFeed;

public class GenerateFeedHandler(KotovskayaMsContext msContext, KotovskayaDbContext dbContext)
    : IRequestHandler<GenerateFeedRequest>
{
    public async Task Handle(GenerateFeedRequest request, CancellationToken cancellationToken)
    {
        
    }
}