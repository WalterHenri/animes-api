using Animes.Infrastructure.Persistence.Contexts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace Animes.API.Tests
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var dbContextOptionsDescriptor = services.SingleOrDefault(
                  d => d.ServiceType == typeof(DbContextOptions<AnimeDbContext>));

                if (dbContextOptionsDescriptor != null)
                {
                    services.Remove(dbContextOptionsDescriptor);
                }

                var dbContextDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(AnimeDbContext));
                if (dbContextDescriptor != null)
                {
                    services.Remove(dbContextDescriptor);
                }

                var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();
                services.AddDbContext<AnimeDbContext>(options =>
                              {
                                  options.UseInMemoryDatabase("InMemoryDbForTesting");
                                  options.UseInternalServiceProvider(serviceProvider); // Chave da solução!
                              });
            });
        }
    }
}