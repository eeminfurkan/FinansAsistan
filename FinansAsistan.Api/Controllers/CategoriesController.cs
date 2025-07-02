using AutoMapper;
using FinansAsistan.Api.Dtos;
using FinansAsistan.Core.Entities;
using FinansAsistan.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinansAsistan.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<CategoryDto>>> GetCategories()
        {
            var categories = await _categoryRepository.GetAllAsync();
            var categoriesDto = _mapper.Map<IReadOnlyList<CategoryDto>>(categories);
            return Ok(categoriesDto);
        }

        [HttpPost]
        public async Task<ActionResult<CategoryDto>> CreateCategory(CreateCategoryDto createCategoryDto)
        {
            var category = _mapper.Map<Category>(createCategoryDto);
            var createdCategory = await _categoryRepository.AddAsync(category);
            var categoryDto = _mapper.Map<CategoryDto>(createdCategory);

            return CreatedAtAction(nameof(GetCategories), new { id = categoryDto.Id }, categoryDto);
        }
    }
}