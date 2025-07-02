using FinansAsistan.Core.Interfaces;
using FinansAsistan.Infrastructure.Repositories;
using FinansAsistan.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using FinansAsistan.Api.Middleware;

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
            builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            // AutoMapper'ý servislere ekliyoruz.
            builder.Services.AddAutoMapper(typeof(Program));
            // MediatR'ý servislere ekliyoruz ve bu projedeki (assembly) tüm Handler'larý otomatik bulmasýný söylüyoruz.
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

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

            app.UseMiddleware<GlobalExceptionHandlerMiddleware>();


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
