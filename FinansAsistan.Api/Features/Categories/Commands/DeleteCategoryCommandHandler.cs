using FinansAsistan.Core.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FinansAsistan.Api.Features.Categories.Commands
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand>
    {
        private readonly ICategoryRepository _categoryRepository;

        public DeleteCategoryCommandHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            await _categoryRepository.DeleteAsync(request.Id);
        }
    }
}