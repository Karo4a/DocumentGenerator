using DocumentGenerator.Context;
using DocumentGenerator.Web.Tests;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DocumentGenerator.Api.Tests.Infrastructure;

/// <summary>
/// Фабрика для интеграционных тестов
/// </summary>
public class TestWebApplicationFactory : WebApplicationFactory<Program>
{
    /// <inheritdoc cref="WebApplicationFactory{TEntryPoint}.CreateHost"/>
    protected override IHost CreateHost(IHostBuilder builder)
    {
        return base.CreateHost(builder);
    }

    /// <inheritdoc cref="WebApplicationFactory{TEntryPoint}.ConfigureWebHost"/>
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestAppConfiguration();
        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<DocumentGeneratorContext>));
            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            services.AddSingleton(provider =>
            {
                var configuration = provider.GetRequiredService<IConfiguration>();
                var connectionString = configuration.GetConnectionString("IntegrationConnection");
                var dbContextOptions = new DbContextOptions<DocumentGeneratorContext>(new Dictionary<Type, IDbContextOptionsExtension>());
                var optionsBuilder = new DbContextOptionsBuilder<DocumentGeneratorContext>(dbContextOptions)
                    .UseApplicationServiceProvider(provider)
                    .UseSqlServer(connectionString: string.Format(connectionString!, Guid.NewGuid().ToString("N")));
                return optionsBuilder.Options;
            });
        });
    }
}
