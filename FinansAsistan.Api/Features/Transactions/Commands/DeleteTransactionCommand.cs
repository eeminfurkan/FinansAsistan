using MediatR;

namespace FinansAsistan.Api.Features.Transactions.Commands
{
    public class DeleteTransactionCommand : IRequest
    {
        public int Id { get; set; }
    }
}