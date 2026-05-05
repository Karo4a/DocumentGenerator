using DocumentGenerator.Context.Contracts;
using DocumentGenerator.Entities;
using DocumentGenerator.Repositories.Contracts.ReadRepositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DocumentGenerator.Repositories.ReadRepositories
{
    /// <inheritdoc cref="IProductReadRepository" />
    public class ProductReadRepository : IProductReadRepository
    {
        private readonly IReader reader;

        /// <summary>
        /// Конструктор
        /// </summary>
        public ProductReadRepository(IReader reader)
        {
            this.reader = reader;
        }

        Task<bool> IProductReadRepository.Any(Expression<Func<Product, bool>> action, CancellationToken cancellationToken)
            => reader.Read<Product>()
                .NotDeletedAt()
                .AnyAsync(action, cancellationToken);

        Task<Product?> IProductReadRepository.GetById(Guid id, CancellationToken cancellationToken)
            => reader.Read<Product>()
                .NotDeletedAt()
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);

        Task<IReadOnlyCollection<Product>> IProductReadRepository.GetByIds(IReadOnlyCollection<Guid> ids, CancellationToken cancellationToken)
            => reader.Read<Product>()
                .NotDeletedAt()
                .ByIds(ids)
                .ToReadOnlyCollectionAsync(cancellationToken);

        Task<IReadOnlyCollection<Product>> IProductReadRepository.GetAll(CancellationToken cancellationToken)
            => reader.Read<Product>()
                .NotDeletedAt()
                .OrderBy(x => x.Name)
                .ToReadOnlyCollectionAsync(cancellationToken);
    }
}
