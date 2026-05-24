using AutoMapper;
using DocumentGenerator.Context.Contracts;
using DocumentGenerator.Entities;
using DocumentGenerator.Repositories.Contracts.ReadRepositories;
using DocumentGenerator.Repositories.Contracts.WriteRepositories;
using DocumentGenerator.Services.Contracts.Exceptions;
using DocumentGenerator.Services.Contracts.IServices;
using DocumentGenerator.Services.Contracts.Models.Product;

namespace DocumentGenerator.Services;


/// <inheritdoc cref="IProductService"/>
public class ProductService : IProductService, IServiceAnchor
{
    private readonly IProductReadRepository productReadRepository;
    private readonly IProductWriteRepository productWriteRepository;
    private readonly IMapper mapper;
    private readonly IUnitOfWork unitOfWork;

    /// <summary>
    /// Конструктор
    /// </summary>
    public ProductService(IProductReadRepository productReadRepository,
        IProductWriteRepository productWriteRepository,
        IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        this.productReadRepository = productReadRepository;
        this.productWriteRepository = productWriteRepository;
        this.mapper = mapper;
        this.unitOfWork = unitOfWork;
    }

    async Task<ProductModel> IProductService.GetById(Guid id, CancellationToken cancellationToken)
    {
        var item = await productReadRepository.GetById(id, cancellationToken)
            ?? throw new DocumentGeneratorNotFoundException($"Не удалось найти товар с идентификатором {id}");
        return mapper.Map<ProductModel>(item);
    }

    async Task<IReadOnlyCollection<ProductModel>> IProductService.GetAll(CancellationToken cancellationToken)
    {
        var items = await productReadRepository.GetAll(cancellationToken);
        return mapper.Map<IReadOnlyCollection<ProductModel>>(items);
    }

    async Task<ProductModel> IProductService.Create(ProductCreateModel model, CancellationToken cancellationToken)
    {
        if (await productReadRepository.Any(x => x.Name == model.Name, cancellationToken))
            throw new DocumentGeneratorDuplicateException($"Товар с именем {model.Name} уже существует.");

        var result = new Product
        {
            Id = Guid.NewGuid(),
            Name = model.Name,
            Cost = model.Cost,
        };
        productWriteRepository.Add(result);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return mapper.Map<ProductModel>(result);
    }

    async Task<ProductModel> IProductService.Edit(Guid id, ProductCreateModel model, CancellationToken cancellationToken)
    {
        if (await productReadRepository.Any(x => x.Name == model.Name && x.Id != id, cancellationToken))
            throw new DocumentGeneratorDuplicateException($"Товар с именем {model.Name} уже существует.");

        var entity = await productReadRepository.GetById(id, cancellationToken)
            ?? throw new DocumentGeneratorNotFoundException($"Не удалось найти товар с идентификатором {id}");

        entity.Name = model.Name;
        entity.Cost = model.Cost;
        entity.UpdatedAt = DateTimeOffset.UtcNow;

        productWriteRepository.Edit(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return mapper.Map<ProductModel>(entity);
    }

    async Task IProductService.Delete(Guid id, CancellationToken cancellationToken)
    {
        var entity = await productReadRepository.GetById(id, cancellationToken)
            ?? throw new DocumentGeneratorNotFoundException($"Не удалось найти товар с идентификатором {id}");
        productWriteRepository.Delete(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
