using DocumentGenerator.Context;
using DocumentGenerator.Web.Tests.Client;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace DocumentGenerator.Web.Tests.Infrastructure
{
    /// <summary>
    /// Фикстура API для интеграционных тестов
    /// </summary>
    public class DocumentGeneratorApiFixture : IAsyncLifetime
    {
        private readonly TestWebApplicationFactory factory;
        private DocumentGeneratorContext? context;

        /// <summary>
        /// Конструтор
        /// </summary>
        public DocumentGeneratorApiFixture()
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
