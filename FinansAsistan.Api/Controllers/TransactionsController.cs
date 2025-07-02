using FinansAsistan.Api.Dtos;
using FinansAsistan.Api.Features.Transactions.Commands;
using FinansAsistan.Api.Features.Transactions.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinansAsistan.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TransactionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransactionDto>>> GetTransactions()
        {
            var result = await _mediator.Send(new GetAllTransactionsQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TransactionDto>> GetTransaction(int id)
        {
            var result = await _mediator.Send(new GetTransactionByIdQuery { Id = id });
            if (result == null) { return NotFound(); }
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<TransactionDto>> PostTransaction(CreateTransactionDto createDto)
        {
            var command = new CreateTransactionCommand
            {
                Description = createDto.Description,
                Amount = createDto.Amount,
                Date = createDto.Date,
                Type = createDto.Type,
                CategoryId = createDto.CategoryId
            };
            var createdDto = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetTransaction), new { id = createdDto.Id }, createdDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTransaction(int id, UpdateTransactionDto updateDto)
        {
            if (id != updateDto.Id)
            {
                return BadRequest("URL ID and Body ID must match.");
            }

            var transaction = await _mediator.Send(new GetTransactionByIdQuery { Id = id });
            if (transaction == null)
            {
                return NotFound();
            }

            var command = new UpdateTransactionCommand
            {
                Id = id,
                Description = updateDto.Description,
                Amount = updateDto.Amount,
                Date = updateDto.Date,
                Type = updateDto.Type,
                CategoryId = updateDto.CategoryId
            };

            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaction(int id)
        {
            var transaction = await _mediator.Send(new GetTransactionByIdQuery { Id = id });
            if (transaction == null)
            {
                return NotFound();
            }

            await _mediator.Send(new DeleteTransactionCommand { Id = id });
            return NoContent();
        }
    }
}