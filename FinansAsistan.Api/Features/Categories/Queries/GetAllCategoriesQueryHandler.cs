using AutoMapper;
using FinansAsistan.Api.Dtos;
using FinansAsistan.Core.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FinansAsistan.Api.Features.Categories.Queries
{
    public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, IReadOnlyList<CategoryDto>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public GetAllCategoriesQueryHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<CategoryDto>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            // 1. Repository'den tüm kategorileri çek.
            var categories = await _categoryRepository.GetAllAsync();

            // 2. Gelen Category listesini CategoryDto listesine çevir.
            return _mapper.Map<IReadOnlyList<CategoryDto>>(categories);
        }
    }
}