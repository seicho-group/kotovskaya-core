using AutoMapper;
using Kotovskaya.Categories.Domain.DTO;
using Kotovskaya.DB.Domain.Context;
using Kotovskaya.DB.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kotovskaya.Categories.Application.Services.GetAllCategoriesTree;

public class GetAllCategoriesTreeHandler(KotovskayaDbContext dbContext, IMapper mapper)
    : IRequestHandler<GetAllCategoriesTreeRequest, GetAllCategoriesTreeResponse>
{
    public async Task<GetAllCategoriesTreeResponse> Handle(GetAllCategoriesTreeRequest request, CancellationToken cancellationToken)
    {
        // taking all visible categories without parents - high-layer categories 
        var categoriesData = await dbContext.Categories
            .Where(cat => cat.IsVisible == true)
            .ToArrayAsync(cancellationToken);

        // making top categories list
        var topCategories = categoriesData.Where(cat => cat.ParentCategory == null);
        var result = mapper.Map<CategoryDtoBranch[]>(topCategories).ToList();
        
        // fill children
        var subCategories = categoriesData.Where(cat => cat.ParentCategory != null);
        result.ForEach(cat =>
        {
            cat.CategoryItems = GetCategoryItems(cat, subCategories.ToList());
        });
        
        return new GetAllCategoriesTreeResponse() {Categories = result};
    }

    private List<CategoryDtoBranch>? GetCategoryItems(CategoryDtoBranch parentBranch, List<Category> possibleChildCategories)
    {
        var subcategories = possibleChildCategories
            .Where(cat => cat.ParentCategory != null && cat.ParentCategory.Id == parentBranch.Id)
            .ToList();
        
        return mapper.Map<List<CategoryDtoBranch>>(subcategories);
    }
}