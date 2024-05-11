using AutoMapper;
using AutoMapper.QueryableExtensions;
using Kotovskaya.Categories.Domain.DTO;
using Kotovskaya.DB.Domain.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kotovskaya.Categories.Application.Services.GetCategoryItems;

public class GetCategoryItemsHandler : IRequestHandler<GetCategoryItemsRequest, GetCategoryItemsResponse>
{
    private readonly KotovskayaDbContext _dbContext;
    private readonly IMapper _mapper;
    public async Task<GetCategoryItemsResponse> Handle(GetCategoryItemsRequest request, CancellationToken cancellationToken)
    {
        var category = await _dbContext.Categories
            .FirstOrDefaultAsync(cat => cat.Id == request.CategoryId);

        if (category == null)
        {
            throw new KeyNotFoundException("Категория с данным айди не найдена");
        }

        var subcategories = await _dbContext.Categories
            .Where(cat => cat.ParentCategory != null && cat.ParentCategory.Id == category.Id)
            .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider)
            .ToArrayAsync();
        
        await Task.Delay(1);
        return new GetCategoryItemsResponse()
        {
            CategoryName = category.Name,
            CategoryId = category.Id,
            CategoryChilds = subcategories
        };
    }

    public GetCategoryItemsHandler(KotovskayaDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
}