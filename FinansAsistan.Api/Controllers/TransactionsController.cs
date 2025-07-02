using FinansAsistan.Core.Entities;
using FinansAsistan.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinansAsistan.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // URL'yi /api/transactions olarak ayarlar
    public class TransactionsController : ControllerBase
    {
        // 1. DbContext'i tutacak olan özel alan (private field)
        private readonly FinansAsistanDbContext _context;

        // 2. Constructor (Yapıcı Metot)
        // Dependency Injection ile DbContext'i talep ediyoruz.
        // Program.cs'te yaptığımız servis kaydı sayesinde ASP.NET Core
        // bu 'context' parametresini bizim için otomatik olarak dolduracak.
        public TransactionsController(FinansAsistanDbContext context)
        {
            _context = context;
        }

        // 3. İlk Endpoint'imiz: Tüm işlemleri getirme
        // Bu metoda bir GET isteği geldiğinde çalışır: GET api/transactions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactions()
        {
            // DbContext'i kullanarak Transactions tablosundaki tüm verileri
            // asenkron olarak bir listeye çevirip geri döndürüyoruz.
            var transactions = await _context.Transactions.ToListAsync();

            // HTTP 200 OK durum kodu ile birlikte veriyi döndür.
            return Ok(transactions);
        }

        // ---- YENİ EKLENEN METOT ----
        // POST: api/transactions
        // Yeni bir işlem eklemek için kullanılır.
        [HttpPost]
        public async Task<ActionResult<Transaction>> PostTransaction(Transaction transaction)
        {
            // 1. Gelen 'transaction' nesnesini DbContext'e eklemesi için işaretliyoruz.
            //    Bu aşamada henüz veritabanına YAZILMAZ, sadece "eklenecek" olarak işaretlenir.
            _context.Transactions.Add(transaction);

            // 2. DbContext üzerinde yapılan tüm değişiklikleri (ekleme, silme, güncelleme)
            //    veritabanına tek bir işlemle, asenkron olarak kaydediyoruz.
            await _context.SaveChangesAsync();

            // 3. Başarılı bir oluşturma işlemi sonucunda standart olarak
            //    HTTP 201 Created durum kodu ve oluşturulan nesnenin kendisi döndürülür.
            //    CreatedAtAction metodu, cevabın "Location" başlığına bu yeni kaynağa
            //    nereden ulaşılabileceğinin URL'sini de ekler (örn: /api/transactions/1).
            return CreatedAtAction(nameof(GetTransactions), new { id = transaction.Id }, transaction);
        }

        // GET: api/transactions/5
        // ID'ye göre tek bir işlem getirmek için kullanılır.
        [HttpGet("{id}")]
        public async Task<ActionResult<Transaction>> GetTransaction(int id)
        {
            // FindAsync metodu, verilen primary key (Id) ile eşleşen kaydı
            // veritabanında çok verimli bir şekilde arar.
            var transaction = await _context.Transactions.FindAsync(id);

            // Eğer o ID ile bir işlem bulunamazsa, standart olarak
            // HTTP 404 Not Found durum kodu döndürülür.
            if (transaction == null)
            {
                return NotFound();
            }

            // İşlem bulunduysa, HTTP 200 OK ve işlemin kendisi döndürülür.
            return Ok(transaction);
        }

        // PUT: api/transactions/5
        // Mevcut bir işlemi güncellemek için kullanılır.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTransaction(int id, Transaction transaction)
        {
            // Gelen isteğin URL'indeki id ile, isteğin gövdesindeki (body)
            // transaction nesnesinin Id'si eşleşmiyorsa, bu hatalı bir istektir.
            if (id != transaction.Id)
            {
                return BadRequest(); // HTTP 400 Bad Request
            }

            // EF Core'a bu nesnenin durumunun "Değiştirildi" (Modified) olduğunu söylüyoruz.
            // Bu sayede SaveChanges'i çağırdığımızda EF Core bir UPDATE sorgusu oluşturur.
            _context.Entry(transaction).State = EntityState.Modified;

            try
            {
                // Değişiklikleri kaydetmeye çalışıyoruz.
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Eğer kaydetmeye çalıştığımız sırada bu kayıt başka birisi
                // tarafından silinmişse (yani veritabanında yoksa), bir istisna alırız.
                // Bu durumu kontrol edip 404 dönmek gerekir.
                if (!TransactionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            // Başarılı güncelleme sonucunda standart olarak
            // HTTP 204 No Content durum kodu döndürülür. Bu, "işlemin başarılı
            // ama sana geri döndürecek bir içeriğim yok" anlamına gelir.
            return NoContent();
        }

        // Bir işlemin var olup olmadığını kontrol etmek için özel bir yardımcı metot.
        private bool TransactionExists(int id)
        {
            return _context.Transactions.Any(e => e.Id == id);
        }

        // DELETE: api/transactions/5
        // Mevcut bir işlemi silmek için kullanılır.
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaction(int id)
        {
            // 1. Önce silinecek işlemin veritabanında olup olmadığını kontrol ediyoruz.
            //    FindAsync bunun için en verimli yoldur.
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction == null)
            {
                // Silinecek işlem bulunamadıysa, 404 Not Found döndürüyoruz.
                return NotFound();
            }

            // 2. EF Core'a bu nesneyi "silinecek" olarak işaretlemesini söylüyoruz.
            //    Bu aşamada henüz veritabanından SİLİNMEZ.
            _context.Transactions.Remove(transaction);

            // 3. Değişiklikleri veritabanına kaydediyoruz. Asıl DELETE sorgusu
            //    bu komutla çalıştırılır.
            await _context.SaveChangesAsync();

            // 4. Başarılı silme işlemi sonucunda standart olarak
            //    HTTP 204 No Content durum kodu döndürülür.
            return NoContent();
        }
    }
}