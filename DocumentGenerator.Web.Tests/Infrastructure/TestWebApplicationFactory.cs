using DocumentGenerator.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;

namespace DocumentGenerator.Web.Tests.Infrastructure
{
    /// <summary>
    /// 
    /// </summary>
    public class TestWebApplicationFactory : WebApplicationFactory<Program>
    {
        /// <inheritdoc cref="WebApplicationFactory{TEntryPoint}.CreateHost"/>
        protected override IHost CreateHost(IHostBuilder builder)
        {
            builder.UseEnvironment("integration_tests");
            return base.CreateHost(builder);
        }

        /// <inheritdoc cref="WebApplicationFactory{TEntryPoint}.ConfigureWebHost"/>
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestAppConfiguration();
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<ProductsContext>));
                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                services.AddSingleton<DbContextOptions<ProductsContext>>(provider =>
                {
                    var configuration = provider.GetRequiredService<IConfiguration>();
                    var connectionString = configuration.GetConnectionString("IntegrationConnection");
                    var dbContextOptions = new DbContextOptions<ProductsContext>(new Dictionary<Type, IDbContextOptionsExtension>());
                    var optionsBuilder = new DbContextOptionsBuilder<ProductsContext>(dbContextOptions)
                        .UseApplicationServiceProvider(provider)
                        .UseNpgsql(connectionString: string.Format(connectionString!, Guid.NewGuid().ToString("N")));
                    return optionsBuilder.Options;
                });
            });
        }
    }
}
