using FinansAsistan.Api.Dtos;
using FinansAsistan.Api.Features.Categories.Commands;
using FinansAsistan.Api.Features.Categories.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinansAsistan.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<CategoryDto>>> GetCategories()
        {
            var categories = await _mediator.Send(new GetAllCategoriesQuery());
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> GetCategory(int id)
        {
            var category = await _mediator.Send(new GetCategoryByIdQuery { Id = id });
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult<CategoryDto>> CreateCategory(CreateCategoryDto createCategoryDto)
        {
            var command = new CreateCategoryCommand { Name = createCategoryDto.Name };
            var createdCategory = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetCategory), new { id = createdCategory.Id }, createdCategory);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, UpdateCategoryDto updateCategoryDto)
        {
            if (id != updateCategoryDto.Id)
            {
                return BadRequest();
            }

            var command = new UpdateCategoryCommand { Id = id, Name = updateCategoryDto.Name };
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            // Önce var olup olmadığını kontrol etmek iyi bir pratiktir.
            var category = await _mediator.Send(new GetCategoryByIdQuery { Id = id });
            if (category == null)
            {
                return NotFound();
            }

            await _mediator.Send(new DeleteCategoryCommand { Id = id });
            return NoContent();
        }
    }
}