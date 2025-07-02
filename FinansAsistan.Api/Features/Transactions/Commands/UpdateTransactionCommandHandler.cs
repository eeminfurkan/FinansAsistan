using AutoMapper;
using FinansAsistan.Core.Interfaces;
using MediatR;
using System; // Exception için eklendi
using System.Threading;
using System.Threading.Tasks;
using FinansAsistan.Api.Exceptions; // Yeni using'i ekle


namespace FinansAsistan.Api.Features.Transactions.Commands
{
    public class UpdateTransactionCommandHandler : IRequestHandler<UpdateTransactionCommand>
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly ICategoryRepository _categoryRepository; // <-- YENİ
        private readonly IMapper _mapper;

        public UpdateTransactionCommandHandler(
            ITransactionRepository transactionRepository,
            ICategoryRepository categoryRepository,
            IMapper mapper)
        {
            _transactionRepository = transactionRepository;
            _categoryRepository = categoryRepository; // <-- YENİ
            _mapper = mapper;
        }

        public async Task Handle(UpdateTransactionCommand request, CancellationToken cancellationToken)
        {
            // ---- YENİ EKLENEN KONTROL ----
            var category = await _categoryRepository.GetByIdAsync(request.CategoryId);
            if (category == null)
            {
                // Artık genel bir Exception değil, kendi özel ValidationException'ımızı fırlatıyoruz.
                throw new ValidationException($"Category with Id '{request.CategoryId}' not found.");
            }
            // ---- KONTROL SONU ----

            var transactionToUpdate = await _transactionRepository.GetByIdAsync(request.Id);
            if (transactionToUpdate == null) { return; }

            _mapper.Map(request, transactionToUpdate);
            await _transactionRepository.UpdateAsync(transactionToUpdate);
        }
    }
}