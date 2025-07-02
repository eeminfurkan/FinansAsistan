using AutoMapper;
using FinansAsistan.Api.Dtos;
using FinansAsistan.Core.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FinansAsistan.Api.Features.Transactions.Queries
{
    public class GetAllTransactionsQueryHandler : IRequestHandler<GetAllTransactionsQuery, IReadOnlyList<TransactionDto>>
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IMapper _mapper;

        public GetAllTransactionsQueryHandler(ITransactionRepository transactionRepository, IMapper mapper)
        {
            _transactionRepository = transactionRepository;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<TransactionDto>> Handle(GetAllTransactionsQuery request, CancellationToken cancellationToken)
        {
            // Eskiden Controller'da olan mantık artık burada!
            var transactions = await _transactionRepository.GetAllAsync();

            var transactionsDto = _mapper.Map<IReadOnlyList<TransactionDto>>(transactions);

            return transactionsDto;
        }
    }
}