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

        // ---- YENİ EKLENEN METOT ----
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Bu satır her zaman en başta olmalı

            // Transaction ve Category arasındaki ilişkiyi burada detaylıca yapılandırıyoruz.
            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Category) // Her Transaction'ın bir (HasOne) Category'si vardır
                .WithMany() // Bir Category'nin çok (WithMany) Transaction'ı olabilir
                .HasForeignKey(t => t.CategoryId) // Yabancı anahtar 'CategoryId' sütunudur
                .OnDelete(DeleteBehavior.Restrict); // EN ÖNEMLİ KISIM: Kategori silindiğinde, bu kuralı ihlal ediyorsa işlemi KISITLA/ENGELLE.
        }

    }
}