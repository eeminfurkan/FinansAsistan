using MediatR;

namespace FinansAsistan.Api.Features.Categories.Commands
{
    public class DeleteCategoryCommand : IRequest
    {
        public int Id { get; set; }
    }
}