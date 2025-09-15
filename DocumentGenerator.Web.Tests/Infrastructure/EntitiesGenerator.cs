using Ahatornn.TestGenerator;
using DocumentGenerator.Context;
using DocumentGenerator.Entities;
using DocumentGenerator.Web.Tests.Client;

namespace DocumentGenerator.Web.Tests.Infrastructure
{
    /// <summary>
    /// Генератор сущностей для интеграционных тестов
    /// </summary>
    public class EntitiesGenerator
    {
        private readonly DocumentGeneratorContext context;

        /// <summary>
        /// Конструктор
        /// </summary>
        public EntitiesGenerator(DocumentGeneratorContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Создает <see cref="Entities.Product"/> со случайными данными
        /// </summary>
        public async Task<Product> Product(DateTimeOffset? deletedAt = null)
        {
            var productRequest = ProductRequestApiModel();
            var product = TestEntityProvider.Shared.Create<Product>(x =>
            {
                x.Name = productRequest.Name!;
                x.Cost = (decimal)productRequest.Cost;
                x.DeletedAt = deletedAt;
            });
            await context.AddAsync(product);
            await context.SaveChangesAsync();

            return product;
        }

        /// <summary>
        /// Создает <see cref="Entities.ProductRequestApiModel"/> со случайными данными
        /// </summary>
        public ProductRequestApiModel ProductRequestApiModel()
            => new() {
                Name = $"Name{Guid.NewGuid()}",
                Cost = Random.Shared.NextDouble() * 10.0,
            };

        /// <summary>
        /// Создает <see cref="Entities.Party"/> со случайными данными
        /// </summary>
        public async Task<Party> Party(DateTimeOffset? deletedAt = null)
        {
            var partyRequest = PartyRequestApiModel();
            var party = TestEntityProvider.Shared.Create<Party>(x =>
            {
                x.Name = partyRequest.Name!;
                x.Job = partyRequest.Job!;
                x.TaxId = partyRequest.TaxId!;
                x.DeletedAt = deletedAt;
            });
            await context.AddAsync(party);
            await context.SaveChangesAsync();

            return party;
        }

        /// <summary>
        /// Создает <see cref="Entities.PartyRequestApiModel"/> со случайными данными
        /// </summary>
        public PartyRequestApiModel PartyRequestApiModel()
            => new()
            {
                Name = $"Name{Guid.NewGuid()}",
                Job = $"Job{Guid.NewGuid()}",
                TaxId = RandomTaxId(),
            };

        /// <summary>
        /// Создает <see cref="Entities.Document"/> со случайными данными
        /// </summary>
        public async Task<Document> Document(DateTimeOffset? deletedAt = null)
        {
            var seller = await Party();
            var buyer = await Party();
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
                products.Add(await DocumentProduct(document, deletedAt));
            }
            document.Products = products;

            await context.AddAsync(document);
            await context.SaveChangesAsync();

            return document;
        }

        /// <summary>
        /// Создает <see cref="Entities.DocumentRequestApiModel"/> со случайными данными
        /// </summary>
        public async Task<DocumentRequestApiModel> DocumentRequestApiModel()
        {
            var seller = await Party();
            var buyer = await Party();
            var products = new List<Product>();
            foreach (var _ in Enumerable.Range(0, Random.Shared.Next(1, 4)))
            {
                products.Add(await Product());
            }

            var date = DateOnly.FromDateTime(DateTime.Now).ToDateTime(TimeOnly.MinValue);
            return new()
            {
                DocumentNumber = $"DocumentNumber{Guid.NewGuid()}",
                ContractNumber = $"ContractNumber{Guid.NewGuid()}",
                Date = date,
                SellerId = seller.Id,
                BuyerId = buyer.Id,
                Products = products.Select(x => DocumentProductRequestApiModel(x)).ToList()
            };
        }

        private async Task<DocumentProduct> DocumentProduct(Document document, DateTimeOffset? deletedAt = null)
        {
            var product = await Product();
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

        private DocumentProductRequestApiModel DocumentProductRequestApiModel(Product product)
            => new()
            {
                ProductId = product.Id,
                Quantity = Random.Shared.Next(1, 10),
                Cost = Random.Shared.NextDouble() * 10.0
            };

        private static string RandomTaxId()
        {
            var possibleLengths = new[] {10, 12};
            var length = possibleLengths[Random.Shared.Next(2)];
            return string.Concat(Enumerable.Range(0, length)
                .Select(_ => Random.Shared.Next(10)));
        }
    }
}
