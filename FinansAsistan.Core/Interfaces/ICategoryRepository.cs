using FinansAsistan.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinansAsistan.Core.Interfaces
{
    public interface ICategoryRepository
    {
        Task<Category> GetByIdAsync(int id);
        Task<IReadOnlyList<Category>> GetAllAsync();
        Task<Category> AddAsync(Category category);
        Task UpdateAsync(Category category);
        Task DeleteAsync(int id);
    }
}