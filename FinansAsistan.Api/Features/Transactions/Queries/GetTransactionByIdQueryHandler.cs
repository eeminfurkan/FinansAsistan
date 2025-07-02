using AutoMapper;
using FinansAsistan.Api.Dtos;
using FinansAsistan.Core.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FinansAsistan.Api.Features.Transactions.Queries
{
    public class GetTransactionByIdQueryHandler : IRequestHandler<GetTransactionByIdQuery, TransactionDto>
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IMapper _mapper;

        public GetTransactionByIdQueryHandler(ITransactionRepository transactionRepository, IMapper mapper)
        {
            _transactionRepository = transactionRepository;
            _mapper = mapper;
        }

        public async Task<TransactionDto> Handle(GetTransactionByIdQuery request, CancellationToken cancellationToken)
        {
            var transaction = await _transactionRepository.GetByIdAsync(request.Id);
            // Not: Handler içinde "NotFound" kontrolü yapmıyoruz. 
            // Eğer transaction null ise, null dönecek. Bu durumu Controller yönetecek.
            return _mapper.Map<TransactionDto>(transaction);
        }
    }
}