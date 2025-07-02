using AutoMapper;
using FinansAsistan.Core.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FinansAsistan.Api.Features.Categories.Commands
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public UpdateCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var categoryToUpdate = await _categoryRepository.GetByIdAsync(request.Id);
            if (categoryToUpdate == null) return;

            _mapper.Map(request, categoryToUpdate);
            await _categoryRepository.UpdateAsync(categoryToUpdate);
        }
    }
}