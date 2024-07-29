using AutoMapper;
using AutoMapper.QueryableExtensions;
using Kotovskaya.DB.Domain.Context;
using Kotovskaya.Shared.Application.Entities.DTO;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kotovskaya.Categories.Application.Services.GetCategoryItems;

public class GetCategoryItemsHandler(KotovskayaDbContext dbContext, IMapper mapper, KotovskayaMsContext msContext)
    : IRequestHandler<GetCategoryItemsRequest, GetCategoryItemsResponse>
{
    public async Task<GetCategoryItemsResponse> Handle(GetCategoryItemsRequest request,
        CancellationToken cancellationToken)
    {
        var category = await dbContext.Categories
            .Include(category => category.Products)!
                .ThenInclude(pr => pr.SaleTypes)
            .FirstOrDefaultAsync(cat => cat.Id == request.CategoryId, cancellationToken);

        if (category == null)
            throw new KeyNotFoundException("Категория с данным айди не найдена");

        var subcategories = await dbContext.Categories
            .Where(cat => cat.ParentCategory != null && cat.ParentCategory.Id == category.Id)
            .ProjectTo<CategoryDto>(mapper.ConfigurationProvider)
            .ToArrayAsync(cancellationToken);

        var items = mapper.Map<ProductEntityDto[]>(category.Products)
            .Where(pr => pr.Quantity > 0)
            .OrderByDescending(pr => pr.Quantity)
            .ToArray();
        
        return new GetCategoryItemsResponse
        {
            CategoryName = category.Name,
            CategoryId = category.Id,
            CategoryItems = items,
            CategoryChildren = subcategories
        };
    }
}
