using AutoMapper;
using DocumentGenerator.Context.Contracts;
using DocumentGenerator.Entities;
using DocumentGenerator.ProductRepository.Contracts;
using DocumentGenerator.Services.Contracts.Exceptions;
using DocumentGenerator.Services.Contracts.Models;
using Microsoft.EntityFrameworkCore;

namespace DocumentGenerator.Services
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
                Description = model.Description,
                CreatedAt = DateTimeOffset.UtcNow,
                UpdatedAt = DateTimeOffset.UtcNow,
                DeletedAt = null,
            };
            productWriteRepository.Add(result);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<ProductModel>(result);
        }

        async Task<ProductModel> IProductServices.Edit(ProductModel model, CancellationToken cancellationToken)
        {
            var entity = await productReadRepository.GetById(model.Id, cancellationToken);
            if (entity == null)
            {
                throw new ProductNotFoundException($"Не удалось найти товар с иденитификатором {model.Id}");
            }

            entity.Name = model.Name;
            entity.Description = model.Description;
            entity.UpdatedAt = DateTimeOffset.UtcNow;

            productWriteRepository.Edit(entity);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return mapper.Map<ProductModel>(entity);
        }
    }
}
