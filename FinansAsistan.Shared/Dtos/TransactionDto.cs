using System;

namespace FinansAsistan.Shared.Dtos
{
    public class TransactionDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public int CategoryId { get; set; }

        // DTO'nun en büyük faydası: İlişkili kategorinin adını da buraya ekleyebiliriz.
        public string CategoryName { get; set; }
    }
}