using AutoMapper;
using FinansAsistan.Api.Dtos;
using FinansAsistan.Core.Entities;
using FinansAsistan.Core.Interfaces;
using MediatR;
using System; // Exception için eklendi
using System.Threading;
using System.Threading.Tasks;
using FinansAsistan.Api.Exceptions; // Yeni using'i ekle


namespace FinansAsistan.Api.Features.Transactions.Commands
{
    public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, TransactionDto>
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly ICategoryRepository _categoryRepository; // <-- YENİ
        private readonly IMapper _mapper;

        // Constructor'ı güncelledik
        public CreateTransactionCommandHandler(
            ITransactionRepository transactionRepository,
            ICategoryRepository categoryRepository,
            IMapper mapper)
        {
            _transactionRepository = transactionRepository;
            _categoryRepository = categoryRepository; // <-- YENİ
            _mapper = mapper;
        }

        public async Task<TransactionDto> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
        {
            // ---- YENİ EKLENEN KONTROL ----
            var category = await _categoryRepository.GetByIdAsync(request.CategoryId);
            if (category == null)
            {
                // Artık genel bir Exception değil, kendi özel ValidationException'ımızı fırlatıyoruz.
                throw new ValidationException($"Category with Id '{request.CategoryId}' not found.");
            }
            // ---- KONTROL SONU ----

            var transactionEntity = _mapper.Map<Transaction>(request);
            var createdTransaction = await _transactionRepository.AddAsync(transactionEntity);

            // DTO'yu döndürmeden önce CategoryName'i manuel olarak atıyoruz,
            // çünkü AddAsync'ten dönen nesnede bu bilgi olmayabilir.
            var transactionDto = _mapper.Map<TransactionDto>(createdTransaction);
            transactionDto.CategoryName = category.Name;

            return transactionDto;
        }
    }
}