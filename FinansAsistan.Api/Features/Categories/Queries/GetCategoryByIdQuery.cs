using FinansAsistan.Api.Dtos;
using MediatR;

namespace FinansAsistan.Api.Features.Categories.Queries
{
    public class GetCategoryByIdQuery : IRequest<CategoryDto>
    {
        public int Id { get; set; }
    }
}