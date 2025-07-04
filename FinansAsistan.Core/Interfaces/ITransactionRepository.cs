using FinansAsistan.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinansAsistan.Core.Interfaces
{
    public interface ITransactionRepository
    {
        Task<Transaction> GetByIdAsync(int id);
        Task<IReadOnlyList<Transaction>> GetAllAsync();
        Task<Transaction> AddAsync(Transaction transaction);
        Task UpdateAsync(Transaction transaction);
        Task DeleteAsync(int id);
        Task<bool> HasTransactionsWithCategoryAsync(int categoryId);

    }
}