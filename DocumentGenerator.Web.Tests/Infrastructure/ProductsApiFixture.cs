using DocumentGenerator.Context;
using DocumentGenerator.Web.Tests.Client;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using Xunit;

namespace DocumentGenerator.Web.Tests.Infrastructure
{
    public class ProductsApiFixture : IAsyncLifetime
    {
        private readonly TestWebApplicationFactory factory;
        private DocumentGeneratorContext? context;

        /// <summary>
        /// Конструтор
        /// </summary>
        public ProductsApiFixture()
        {
            factory = new TestWebApplicationFactory();
        }

        internal DocumentGeneratorContext Context
        {
            get
            {
                if (context != null)
                {
                    return context;
                }

                var scope = factory.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
                context = scope.ServiceProvider.GetRequiredService<DocumentGeneratorContext>();
                return context;
            }
        }

        internal IDocumentGeneratorApiClient WebClient
        {
            get
            {
                var client = factory.CreateClient();
                return new DocumentGeneratorApiClient(string.Empty, client);
            }
        }

        /// <summary>
        /// Асинхронная инициализация
        /// </summary>
        public Task InitializeAsync() => Context.Database.MigrateAsync();

        /// <summary>
        /// Асинхронное освобождение
        /// </summary>
        public async Task DisposeAsync()
        {
            await Context.Database.EnsureDeletedAsync();
            await Context.Database.CloseConnectionAsync();
            await Context.DisposeAsync();
            await factory.DisposeAsync();
        }
    }
}
