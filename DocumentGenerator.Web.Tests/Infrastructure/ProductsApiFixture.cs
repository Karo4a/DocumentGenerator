using DocumentGenerator.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using Xunit;

namespace DocumentGenerator.Web.Tests.Infrastructure
{
    public class ProductsApiFixture : IAsyncLifetime
    {
        private readonly TestWebApplicationFactory factory;
        private ProductsContext? context;

        /// <summary>
        /// Конструтор
        /// </summary>
        public ProductsApiFixture()
        {
            factory = new TestWebApplicationFactory();
        }

        internal ProductsContext Context
        {
            get
            {
                if (context != null)
                {
                    return context;
                }

                var scope = factory.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
                context = scope.ServiceProvider.GetRequiredService<ProductsContext>();
                return context;
            }
        }

        internal HttpClient WebClient => factory.CreateClient();

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
