using DocumentGenerator.Entities;
using System.Linq.Expressions;

namespace DocumentGenerator.Repositories.Contracts.ReadRepositories
{
    /// <summary>
    /// Репозиторий чтения сущности <see cref="Product"/>
    /// </summary>
    public interface IProductReadRepository
    {
        /// <summary>
        /// Возвращает true, если хотя бы один <see cref="Product"> в БД удовлетворяет условию, иначе false
        /// </summary>
        Task<bool> Any(Expression<Func<Product, bool>> action, CancellationToken cancellationToken);

        /// <summary>
        /// Получает <see cref="Product"/> по идентификатору
        /// </summary>
        Task<Product?> GetById(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получает коллекцию <see cref="Product"/> по идентификаторам
        /// </summary>
        Task<IReadOnlyCollection<Product>> GetByIds(IReadOnlyCollection<Guid> ids, CancellationToken cancellationToken);

        /// <summary>
        /// Получает коллекцию <see cref="Product"/>
        /// </summary>
        Task<IReadOnlyCollection<Product>> GetAll(CancellationToken cancellationToken);
    }
}
