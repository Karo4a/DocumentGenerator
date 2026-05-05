using Ahatornn.TestGenerator;
using DocumentGenerator.Context.Contracts;
using DocumentGenerator.Entities;
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

        /// <summary>
        /// Подготавливает <see cref="Product"/> в БД для тестов
        /// </summary>
        public async Task<Product> PrepareProduct(DateTimeOffset? deletedAt = null, bool save = true)
        {
            var product = TestEntityProvider.Shared.Create<Product>(x =>
            {
                x.Cost = (decimal)Random.Shared.NextDouble() * 10.0m;
                x.DeletedAt = deletedAt;
            });
            if (save)
            {
                await Context.AddAsync(product);
                await UnitOfWork.SaveChangesAsync();
            }

            return product;
        }

        /// <summary>
        /// Подготавливает <see cref="Party"/> в БД для тестов
        /// </summary>
        public async Task<Party> PrepareParty(DateTimeOffset? deletedAt = null, bool save = true)
        {
            var party = TestEntityProvider.Shared.Create<Party>(x =>
            {
                x.TaxId = RandomTaxId();
                x.DeletedAt = deletedAt;
            });
            if (save)
            {
                await Context.AddAsync(party);
                await UnitOfWork.SaveChangesAsync();
            }

            return party;
        }

        /// <summary>
        /// Подготавливает <see cref="Document"/> в БД для тестов
        /// </summary>
        public async Task<Document> PrepareDocument(DateTimeOffset? deletedAt = null, bool save = true)
        {
            var seller = await PrepareParty(save: !save);
            var buyer = await PrepareParty(save: !save);
            var document = TestEntityProvider.Shared.Create<Document>(x =>
            {
                x.Date = DateOnly.FromDateTime(DateTime.Now);
                x.SellerId = seller.Id;
                x.BuyerId = buyer.Id;
                x.Seller = seller;
                x.Buyer = buyer;
                x.DeletedAt = deletedAt;
            });
            var products = new List<DocumentProduct>();
            foreach (var _ in Enumerable.Range(0, Random.Shared.Next(1, 4)))
            {
                products.Add(await PrepareDocumentProduct(document, deletedAt, !save));
            }
            document.Products = products;

            if (save)
            {
                await Context.AddAsync(document);
                await UnitOfWork.SaveChangesAsync();
            }

            return document;
        }

        private async Task<DocumentProduct> PrepareDocumentProduct(Document document, DateTimeOffset? deletedAt = null, bool save = true)
        {
            var product = await PrepareProduct(save: save);
            return TestEntityProvider.Shared.Create<DocumentProduct>(x =>
            {
                x.Product = product;
                x.ProductId = x.Product.Id;
                x.DocumentId = document.Id;
                x.Quantity = Random.Shared.Next(1, 10);
                x.Document = document;
                x.DeletedAt = deletedAt;
            });
        }

        private static string RandomTaxId()
        {
            var possibleLengths = new[] { 10, 12 };
            var length = possibleLengths[Random.Shared.Next(2)];
            return string.Concat(Enumerable.Range(0, length)
                .Select(_ => Random.Shared.Next(10)));
        }

        /// <inheritdoc cref="IAsyncDisposable" />
        public async ValueTask DisposeAsync()
        {
            await Context.Database.EnsureDeletedAsync();
            await Context.DisposeAsync();
        }
    }
}
