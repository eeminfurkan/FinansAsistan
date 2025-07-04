using FinansAsistan.Core.Entities;
using FinansAsistan.Core.Interfaces;
using FinansAsistan.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinansAsistan.Infrastructure.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly FinansAsistanDbContext _context;

        public TransactionRepository(FinansAsistanDbContext context)
        {
            _context = context;
        }

        public async Task<Transaction> AddAsync(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
            return transaction;
        }

        public async Task DeleteAsync(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction != null)
            {
                _context.Transactions.Remove(transaction);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IReadOnlyList<Transaction>> GetAllAsync()
        {
            // .Include() ile Category verisini de getirmesini söylüyoruz.
            return await _context.Transactions.Include(t => t.Category).ToListAsync();
        }

        public async Task<Transaction> GetByIdAsync(int id)
        {
            // .Include() ile Category verisini de getirip, sonra ID'ye göre filtreliyoruz.
            return await _context.Transactions
                .Include(t => t.Category)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task UpdateAsync(Transaction transaction)
        {
            _context.Entry(transaction).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<bool> HasTransactionsWithCategoryAsync(int categoryId)
        {
            return await _context.Transactions.AnyAsync(t => t.CategoryId == categoryId);
        }
    }
}