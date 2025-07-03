using FinansAsistan.Api.Exceptions; // ValidationException için
using FinansAsistan.Core.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FinansAsistan.Api.Features.Categories.Commands
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ITransactionRepository _transactionRepository; // <-- YENİ

        public DeleteCategoryCommandHandler(ICategoryRepository categoryRepository, ITransactionRepository transactionRepository) // <-- GÜNCELLENDİ
        {
            _categoryRepository = categoryRepository;
            _transactionRepository = transactionRepository; // <-- YENİ
        }

        public async Task Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            // ---- YENİ EKLENEN İŞ KURALI KONTROLÜ ----
            var hasTransactions = await _transactionRepository.HasTransactionsWithCategoryAsync(request.Id);
            if (hasTransactions)
            {
                // Eğer bu kategoriye ait işlemler varsa, kendi ValidationException'ımızı fırlatıyoruz.
                // Middleware'imiz bunu yakalayıp 400 Bad Request'e çevirecek.
                throw new ValidationException("Bu kategoriye atanmış işlemler bulunmaktadır. Silmeden önce işlemleri başka bir kategoriye taşıyın.");
            }
            // ---- KONTROL SONU ----

            await _categoryRepository.DeleteAsync(request.Id);
        }
    }
}