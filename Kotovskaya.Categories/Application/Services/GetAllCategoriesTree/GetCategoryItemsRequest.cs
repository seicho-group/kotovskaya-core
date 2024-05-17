using Kotovskaya.Shared.Application.Entities.DTO;
using MediatR;

namespace Kotovskaya.Categories.Application.Services.GetAllCategoriesTree;

public class GetAllCategoriesTreeRequest:IRequest<List<CategoryDtoBranch>>
{
}