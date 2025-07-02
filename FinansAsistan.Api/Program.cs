using FinansAsistan.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
namespace FinansAsistan.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            // ---- YENÝ EKLENEN BÖLÜM BAÞLANGICI ----

            // 1. Connection string'i appsettings.json dosyasýndan okuyoruz.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            // 2. Entity Framework DbContext'i servislere ekliyoruz.
            //    Uygulamaya SQL Server kullanacaðýmýzý ve hangi connection string'i
            //    kullanacaðýný burada belirtiyoruz.
            builder.Services.AddDbContext<FinansAsistanDbContext>(options =>
                options.UseSqlServer(connectionString));

            // ---- YENÝ EKLENEN BÖLÜM SONU ----

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // ... builder ile ilgili kodlar yukarýda ...

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            // Geliþtirme ortamýndaysak, Swagger'ý (API test arayüzü) etkinleþtir.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Gelen istekleri HTTPS'e yönlendir. Bu bir güvenlik standardýdýr.
            // EKSÝK OLABÝLECEK SATIR
            app.UseHttpsRedirection();

            // Yetkilendirme (Authorization) katmanýný etkinleþtir.
            // Authentication (kimlik doðrulama) eklediðimizde bu önem kazanacak.
            app.UseAuthorization();

            // Gelen istekleri doðru Controller'a yönlendirmek için kullanýlýr.
            app.MapControllers();

            // Uygulamayý çalýþtýr.
            app.Run();
        }
    }
}
