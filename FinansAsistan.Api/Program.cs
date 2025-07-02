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

            // ---- YEN� EKLENEN B�L�M BA�LANGICI ----

            // 1. Connection string'i appsettings.json dosyas�ndan okuyoruz.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            // 2. Entity Framework DbContext'i servislere ekliyoruz.
            //    Uygulamaya SQL Server kullanaca��m�z� ve hangi connection string'i
            //    kullanaca��n� burada belirtiyoruz.
            builder.Services.AddDbContext<FinansAsistanDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            // AutoMapper'� servislere ekliyoruz.
            builder.Services.AddAutoMapper(typeof(Program));
            // MediatR'� servislere ekliyoruz ve bu projedeki (assembly) t�m Handler'lar� otomatik bulmas�n� s�yl�yoruz.
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

            // ---- YEN� EKLENEN B�L�M SONU ----

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // ... builder ile ilgili kodlar yukar�da ...

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            // Geli�tirme ortam�ndaysak, Swagger'� (API test aray�z�) etkinle�tir.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<GlobalExceptionHandlerMiddleware>();


            // Gelen istekleri HTTPS'e y�nlendir. Bu bir g�venlik standard�d�r.
            // EKS�K OLAB�LECEK SATIR
            app.UseHttpsRedirection();

            // Yetkilendirme (Authorization) katman�n� etkinle�tir.
            // Authentication (kimlik do�rulama) ekledi�imizde bu �nem kazanacak.
            app.UseAuthorization();

            // Gelen istekleri do�ru Controller'a y�nlendirmek i�in kullan�l�r.
            app.MapControllers();

            // Uygulamay� �al��t�r.
            app.Run();
        }
    }
}
