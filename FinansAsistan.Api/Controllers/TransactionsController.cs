using FinansAsistan.Shared.Dtos;
using FinansAsistan.Api.Features.Transactions.Commands;
using FinansAsistan.Api.Features.Transactions.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;

namespace FinansAsistan.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public TransactionsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
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
            // Manuel atama yerine doğrudan AutoMapper'ı kullanıyoruz.
            var command = _mapper.Map<CreateTransactionCommand>(createDto);

            var createdTransactionDto = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetTransaction), new { id = createdTransactionDto.Id }, createdTransactionDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTransaction(int id, UpdateTransactionDto updateDto)
        {
            // URL'den gelen ID ile DTO'dan gelen ID'nin tutarlılığını kontrol etmek iyi bir pratiktir.
            if (id != updateDto.Id)
            {
                return BadRequest("URL ID must match the ID in the request body.");
            }

            // Önce kaydın var olup olmadığını kontrol edelim.
            var transactionExists = await _mediator.Send(new GetTransactionByIdQuery { Id = id });
            if (transactionExists == null)
            {
                return NotFound();
            }

            // DTO'yu doğrudan Command'e çeviriyoruz.
            var command = _mapper.Map<UpdateTransactionCommand>(updateDto);

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