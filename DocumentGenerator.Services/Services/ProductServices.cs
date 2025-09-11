using AutoMapper;
using DocumentGenerator.Context.Contracts;
using DocumentGenerator.Entities;
using DocumentGenerator.Repositories.Contracts.ReadRepositories;
using DocumentGenerator.Repositories.Contracts.WriteRepositories;
using DocumentGenerator.Services.Contracts.Exceptions;
using DocumentGenerator.Services.Contracts.IServices;
using DocumentGenerator.Services.Contracts.Models.Product;

namespace DocumentGenerator.Services.Services
{

    /// <inheritdoc cref="IProductServices"/>
    public class ProductServices : IProductServices
    {
        private readonly IProductReadRepository productReadRepository;
        private readonly IProductWriteRepository productWriteRepository;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// Конструктор
        /// </summary>
        public ProductServices(IProductReadRepository productReadRepository,
            IProductWriteRepository productWriteRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            this.productReadRepository = productReadRepository;
            this.productWriteRepository = productWriteRepository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        async Task<ProductModel> IProductServices.GetById(Guid id, CancellationToken cancellationToken)
        {
            var item = await productReadRepository.GetById(id, cancellationToken)
                ?? throw new DocumentGeneratorNotFoundException($"Не удалось найти товар с идентификатором {id}");
            return mapper.Map<ProductModel>(item);
        }

        async Task<IReadOnlyCollection<ProductModel>> IProductServices.GetAll(CancellationToken cancellationToken)
        {
            var items = await productReadRepository.GetAll(cancellationToken);
            return mapper.Map<IReadOnlyCollection<ProductModel>>(items);
        }

        async Task<ProductModel> IProductServices.Create(ProductCreateModel model, CancellationToken cancellationToken)
        {
            var result = new Product
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                Cost = model.Cost,
                CreatedAt = DateTimeOffset.UtcNow,
                UpdatedAt = DateTimeOffset.UtcNow,
                DeletedAt = null,
            };
            productWriteRepository.Add(result);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<ProductModel>(result);
        }

        async Task<ProductModel> IProductServices.Edit(Guid id, ProductCreateModel model, CancellationToken cancellationToken)
        {
            var entity = await productReadRepository.GetById(id, cancellationToken)
                ?? throw new DocumentGeneratorNotFoundException($"Не удалось найти товар с идентификатором {id}");

            entity.Name = model.Name;
            entity.Cost = model.Cost;
            entity.UpdatedAt = DateTimeOffset.UtcNow;

            productWriteRepository.Edit(entity);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return mapper.Map<ProductModel>(entity);
        }

        async Task IProductServices.Delete(Guid id, CancellationToken cancellationToken)
        {
            var entity = await productReadRepository.GetById(id, cancellationToken)
                ?? throw new DocumentGeneratorNotFoundException($"Не удалось найти товар с идентификатором {id}");
            productWriteRepository.Delete(entity);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
