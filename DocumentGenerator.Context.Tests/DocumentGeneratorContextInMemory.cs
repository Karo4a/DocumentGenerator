using DocumentGenerator.Context.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace DocumentGenerator.Context.Tests
{
    /// <summary>
    /// Абстрактный класс для тестов с базой в памяти
    /// </summary>
    public abstract class DocumentGeneratorContextInMemory : IAsyncDisposable
    {
        /// <summary>
        /// Контекст <see cref="DocumentGeneratorContext"/>
        /// </summary>
        protected DocumentGeneratorContext Context { get; }

        /// <inheritdoc cref="IUnitOfWork" />
        protected IUnitOfWork UnitOfWork => Context;

        /// <summary>
        /// Конструктор
        /// </summary>
        protected DocumentGeneratorContextInMemory()
        {
            var optionsBuilder = new DbContextOptionsBuilder<DocumentGeneratorContext>()
                .UseInMemoryDatabase($"DocumentGeneratorContext{Guid.NewGuid()}")
                .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning));
            Context = new DocumentGeneratorContext(optionsBuilder.Options);
        }

        /// <inheritdoc cref="IAsyncDisposable" />
        public async ValueTask DisposeAsync()
        {
            await Context.Database.EnsureDeletedAsync();
            await Context.DisposeAsync();
        }
    }
}
