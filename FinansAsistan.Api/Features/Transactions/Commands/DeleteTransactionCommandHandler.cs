using FinansAsistan.Core.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FinansAsistan.Api.Features.Transactions.Commands
{
    public class DeleteTransactionCommandHandler : IRequestHandler<DeleteTransactionCommand>
    {
        private readonly ITransactionRepository _transactionRepository;

        public DeleteTransactionCommandHandler(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task Handle(DeleteTransactionCommand request, CancellationToken cancellationToken)
        {
            await _transactionRepository.DeleteAsync(request.Id);
        }
    }
}