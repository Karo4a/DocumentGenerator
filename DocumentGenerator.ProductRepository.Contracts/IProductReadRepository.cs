using DocumentGenerator.Entities;

namespace DocumentGenerator.ProductRepository.Contracts
{
    /// <summary>
    /// Интерфейс чтения сущности <see cref="Product"/>
    /// </summary>
    public interface IProductReadRepository
    {
        /// <summary>
        /// Получает <see cref="Product"/> по идентификатору
        /// </summary>
        Task<Product?> GetById(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получает коллекцию <see cref="Product"/>
        /// </summary>
        Task<IReadOnlyCollection<Product>> GetAll(CancellationToken cancellationToken);
    }
}
