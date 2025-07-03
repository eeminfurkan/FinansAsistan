using FinansAsistan.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinansAsistan.Core.Interfaces
{
    public interface ITransactionRepository
    {

        // ---- YENİ EKLENEN METOT ----
        public async Task<bool> HasTransactionsWithCategoryAsync(int categoryId)
        {
            return await _context.Transactions.AnyAsync(t => t.CategoryId == categoryId);
        }

        Task<Transaction> GetByIdAsync(int id);
        Task<IReadOnlyList<Transaction>> GetAllAsync();
        Task<Transaction> AddAsync(Transaction transaction);
        Task UpdateAsync(Transaction transaction);
        Task DeleteAsync(int id);
    }
}