using FinansAsistan.Shared.Dtos;
using MediatR;

namespace FinansAsistan.Api.Features.Transactions.Queries
{
    public class GetTransactionByIdQuery : IRequest<TransactionDto>
    {
        public int Id { get; set; }
    }
}