using AutoMapper;
using FinansAsistan.Shared.Dtos;
using FinansAsistan.Core.Entities;
using FinansAsistan.Core.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FinansAsistan.Api.Features.Categories.Commands
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CategoryDto>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CreateCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<CategoryDto> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            // 1. Gelen komutu bir veritabanı varlığına (entity) çevir.
            var categoryEntity = _mapper.Map<Category>(request);

            // 2. Repository aracılığıyla veritabanına ekle.
            var createdCategory = await _categoryRepository.AddAsync(categoryEntity);

            // 3. Veritabanından dönen (artık bir ID'si olan) nesneyi
            //    kullanıcıya göstereceğimiz DTO'ya çevir ve döndür.
            return _mapper.Map<CategoryDto>(createdCategory);
        }
    }
}