using Kotovskaya.Categories.Domain.DTO;
using MediatR;

namespace Kotovskaya.Categories.Application.Services.GetAllCategoriesTree;

public class GetAllCategoriesTreeRequest:IRequest<List<CategoryDtoBranch>>
{
}