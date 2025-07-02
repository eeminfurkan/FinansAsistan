using MediatR;

namespace FinansAsistan.Api.Features.Categories.Commands
{
    public class UpdateCategoryCommand : IRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}