using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace FinansAsistan.Infrastructure.Data
{
    public class FinansAsistanDbContextFactory : IDesignTimeDbContextFactory<FinansAsistanDbContext>
    {
        public FinansAsistanDbContext CreateDbContext(string[] args)
        {
            // Bu fabrika, komut satırı araçları tarafından çağrıldığında
            // appsettings.json dosyasını bulup okumak için manuel bir yol izler.

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../FinansAsistan.Api"))
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<FinansAsistanDbContext>();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            builder.UseSqlServer(connectionString);

            return new FinansAsistanDbContext(builder.Options);
        }
    }
}