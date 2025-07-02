using FinansAsistan.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinansAsistan.Infrastructure.Data
{
    public class FinansAsistanDbContext : DbContext
    {
        public FinansAsistanDbContext(DbContextOptions<FinansAsistanDbContext> options) : base(options)
        {
        }

        // Bu DbSet, veritabanındaki "Transactions" tablosunu temsil edecek.
        public DbSet<Transaction> Transactions { get; set; }

        // Gelecekte buraya yeni tablolarımızı ekleyeceğiz.
        public DbSet<Category> Categories { get; set; }
        // public DbSet<Budget> Budgets { get; set; }
    }
}