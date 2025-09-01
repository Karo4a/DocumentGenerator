using DocumentGenerator.Context.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace DocumentGenerator.Context.Tests
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class ProductsContextInMemory : IAsyncDisposable
    {
        /// <summary>
        /// Контекст <see cref="ProductsContext"/>
        /// </summary>
        protected ProductsContext Context { get; }

        /// <inheritdoc cref="IUnitOfWork" />
        protected IUnitOfWork UnitOfWork => Context;

        /// <summary>
        /// Конструктор
        /// </summary>
        protected ProductsContextInMemory()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ProductsContext>()
                .UseInMemoryDatabase($"ProductContext{Guid.NewGuid()}")
                .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning));
            Context = new ProductsContext(optionsBuilder.Options);
        }

        /// <inheritdoc cref="IAsyncDisposable" />
        public async ValueTask DisposeAsync()
        {
            await Context.Database.EnsureDeletedAsync();
            await Context.DisposeAsync();
        }
    }
}
