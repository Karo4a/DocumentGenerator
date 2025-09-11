using AutoMapper;
using DocumentGenerator.Context.Contracts;
using DocumentGenerator.Entities;
using DocumentGenerator.Repositories.Contracts.ReadRepositories;
using DocumentGenerator.Repositories.Contracts.WriteRepositories;
using DocumentGenerator.Services.Contracts.Exceptions;
using DocumentGenerator.Services.Contracts.IServices;
using DocumentGenerator.Services.Contracts.Models.Document;

namespace DocumentGenerator.Services
{

    /// <inheritdoc cref="IDocumentServices"/>
    public class DocumentServices : IDocumentServices
    {
        private readonly IDocumentReadRepository documentReadRepository;
        private readonly IDocumentWriteRepository documentWriteRepository;
        private readonly IDocumentProductWriteRepository documentProductWriteRepository;
        private readonly IPartyReadRepository partyReadRepository;
        private readonly IProductReadRepository productReadRepository;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// Конструктор
        /// </summary>
        public DocumentServices(IDocumentReadRepository documentReadRepository,
            IDocumentWriteRepository documentWriteRepository,
            IDocumentProductWriteRepository documentProductWriteRepository,
            IPartyReadRepository partyReadRepository,
            IProductReadRepository productReadRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            this.documentReadRepository = documentReadRepository;
            this.documentWriteRepository = documentWriteRepository;
            this.documentProductWriteRepository = documentProductWriteRepository;
            this.partyReadRepository = partyReadRepository;
            this.productReadRepository = productReadRepository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        async Task<DocumentModel> IDocumentServices.GetById(Guid id, CancellationToken cancellationToken)
        {
            var entity = await documentReadRepository.GetById(id, cancellationToken)
                ?? throw new DocumentGeneratorNotFoundException($"Не удалось найти документ с идентификатором {id}");
            return mapper.Map<DocumentModel>(entity);
        }

        async Task<IReadOnlyCollection<DocumentModel>> IDocumentServices.GetAll(CancellationToken cancellationToken)
        {
            var items = await documentReadRepository.GetAll(cancellationToken);
            return mapper.Map<IReadOnlyCollection<DocumentModel>>(items);
        }

        async Task<DocumentModel> IDocumentServices.Create(DocumentCreateModel model, CancellationToken cancellationToken)
        {
            await ValidateConnections(model, cancellationToken);

            var result = new Document
            {
                Id = Guid.NewGuid(),
                DocumentNumber = model.DocumentNumber,
                ContractNumber = model.ContractNumber,
                Date = model.Date,
                SellerId = model.SellerId,
                BuyerId = model.BuyerId,
            };

            var products = mapper.Map<ICollection<DocumentProduct>>(model.Products);
            foreach (var product in products)
            {
                product.Id = Guid.NewGuid();
                product.DocumentId = result.Id;
                documentProductWriteRepository.Add(product);
            }

            documentWriteRepository.Add(result);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            var createdEntity = await documentReadRepository.GetById(result.Id, cancellationToken)
                ?? throw new InvalidOperationException($"Не удалось найти документ с идентификатором {result.Id}");

            return mapper.Map<DocumentModel>(createdEntity);
        }

        async Task<DocumentModel> IDocumentServices.Edit(Guid id, DocumentCreateModel model, CancellationToken cancellationToken)
        {
            await ValidateConnections(model, cancellationToken);

            var entity = await documentReadRepository.GetById(id, cancellationToken)
                ?? throw new DocumentGeneratorNotFoundException($"Не удалось найти документ с идентификатором {id}");

            entity.DocumentNumber = model.DocumentNumber;
            entity.ContractNumber = model.ContractNumber;
            entity.Date = model.Date;
            entity.SellerId = model.SellerId;
            entity.BuyerId = model.BuyerId;
            entity.UpdatedAt = DateTimeOffset.UtcNow;

            var products = mapper.Map<ICollection<DocumentProduct>>(model.Products);
            var existingProducts = entity.Products;
            var existingProductsDictionary = existingProducts.ToDictionary(x => x.ProductId);

            var comparer = EqualityComparer<DocumentProduct>.Create(
                (x, y) => x!.ProductId == y!.ProductId,
                obj => obj.ProductId.GetHashCode()
            );

            foreach (var product in products)
            {
                if (existingProductsDictionary.TryGetValue(product.ProductId, out var foundProduct))
                {
                    foundProduct.Quantity = product.Quantity;
                    foundProduct.Cost = product.Cost;
                    documentProductWriteRepository.Edit(foundProduct);
                } else
                {
                    product.DocumentId = id;
                    documentProductWriteRepository.Add(product);
                }
            }

            foreach (var exceptProduct in existingProducts.Except(products, comparer))
            {
                documentProductWriteRepository.Delete(existingProductsDictionary[exceptProduct.ProductId]);
            }

            documentWriteRepository.Edit(entity);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            var editedEntity = await documentReadRepository.GetById(id, cancellationToken)
                ?? throw new InvalidOperationException($"Не удалось найти документ с идентификатором {id}");

            return mapper.Map<DocumentModel>(editedEntity);
        }

        async Task IDocumentServices.Delete(Guid id, CancellationToken cancellationToken)
        {
            var entity = await documentReadRepository.GetById(id, cancellationToken)
                ?? throw new DocumentGeneratorNotFoundException($"Не удалось найти документ с идентификатором {id}");

            foreach (var product in entity.Products)
            {
                documentProductWriteRepository.Delete(product);
            }

            documentWriteRepository.Delete(entity);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        private async Task ValidateConnections(DocumentCreateModel model, CancellationToken cancellationToken)
        {
            if ((await documentReadRepository.GetAll(cancellationToken)).Any(x => x.DocumentNumber == model.DocumentNumber))
            {
                throw new DocumentGeneratorDuplicateException($"Документ с номер {model.DocumentNumber} уже существует");
            }
            else if (await partyReadRepository.GetById(model.SellerId, cancellationToken) == null)
            {
                throw new DocumentGeneratorNotFoundException($"Не удалось найти продавца с идентификатором {model.SellerId}");
            }
            else if (await partyReadRepository.GetById(model.BuyerId, cancellationToken) == null)
            {
                throw new DocumentGeneratorNotFoundException($"Не удалось найти покупателя с идентификатором {model.BuyerId}");
            }

            var missingProductIds = new List<Guid>();
            foreach (var product in model.Products)
            {
                if (await productReadRepository.GetById(product.ProductId, cancellationToken) == null)
                {
                    missingProductIds.Add(product.ProductId);
                }
            }

            if (missingProductIds.Count > 0)
            {
                throw new DocumentGeneratorNotFoundException($"Не удалось найти товары с идентификаторами: {string.Join(", ", missingProductIds)}");
            }
        }
    }
}
