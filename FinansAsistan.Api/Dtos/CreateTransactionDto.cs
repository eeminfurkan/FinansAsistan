using System;

namespace FinansAsistan.Api.Dtos
{
    public class CreateTransactionDto
    {
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; } // "Income" veya "Expense"
        public int CategoryId { get; set; } // Kullanıcının hangi kategoriyi seçtiğini belirtmesi için.
    }
}