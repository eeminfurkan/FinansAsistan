using System;

namespace FinansAsistan.Shared.Dtos
{
    public class UpdateTransactionDto
    {
        // Güncellenecek kaydın ID'sini de buraya ekliyoruz ki,
        // Controller'da URL'den gelen ID ile gövdeden gelen ID'yi karşılaştırabilelim.
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public int CategoryId { get; set; }
    }
}