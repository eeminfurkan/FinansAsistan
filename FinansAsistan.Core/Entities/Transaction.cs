using System;

namespace FinansAsistan.Core.Entities
{
    public class Transaction
    {
        // Veritabanındaki benzersiz anahtar (Primary Key)
        public int Id { get; set; }

        // İşlemin açıklaması (örn: "Market Alışverişi")
        public string Description { get; set; }

        // İşlemin tutarı. Para birimleri için her zaman 'decimal' kullanırız.
        public decimal Amount { get; set; }

        // İşlemin yapıldığı tarih
        public DateTime Date { get; set; }

        // İşlemin türü: "Income" (Gelir) veya "Expense" (Gider)
        // Bunu ileride daha gelişmiş bir yapıya (Enum) çevirebiliriz.
        public string Type { get; set; }

        // ---- YENİ EKLENEN SATIRLAR ----
        public int CategoryId { get; set; } // Foreign Key (Yabancı Anahtar)
        public Category Category { get; set; } // Navigation Property (Gezinme Özelliği)
    }
}