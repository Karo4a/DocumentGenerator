using DocumentGenerator.Context.Contracts;
using DocumentGenerator.Entities;
using DocumentGenerator.Repositories.Contracts.ReadRepositories;
using Microsoft.EntityFrameworkCore;

namespace DocumentGenerator.Repositories.ReadRepositories
{

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

        Task<Product?> IProductReadRepository.GetById(Guid id, CancellationToken cancellationToken)
            => reader.Read<Product>()
                .NotDeletedAt()
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);

        Task<IReadOnlyCollection<Product>> IProductReadRepository.GetAll(CancellationToken cancellationToken)
            => reader.Read<Product>()
                .NotDeletedAt()
                .OrderBy(x => x.Name)
                .ToReadOnlyCollectionAsync(cancellationToken);
    }
}
