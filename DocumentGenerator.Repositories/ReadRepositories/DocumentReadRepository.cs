using DocumentGenerator.Context.Contracts;
using DocumentGenerator.Entities;
using DocumentGenerator.Repositories.Contracts.ReadRepositories;
using Microsoft.EntityFrameworkCore;

namespace DocumentGenerator.Repositories.ReadRepositories
{
    /// <inheritdoc cref="IDocumentReadRepository" />
    public class DocumentReadRepository : IDocumentReadRepository
    {
        private readonly IReader reader;

        /// <summary>
        /// Конструктор
        /// </summary>
        public DocumentReadRepository(IReader reader)
        {
            this.reader = reader;
        }

        Task<Document?> IDocumentReadRepository.GetById(Guid id, CancellationToken cancellationToken)
            => reader.Read<Document>()
                .NotDeletedAt()
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);

        Task<IReadOnlyCollection<Document>> IDocumentReadRepository.GetAll(CancellationToken cancellationToken)
            => reader.Read<Document>()
                .NotDeletedAt()
                .Include(x => x.Products)
                    .ThenInclude(x => x.Product)
                .OrderBy(x => x.DocumentNumber)
                .ToReadOnlyCollectionAsync(cancellationToken);
    }
}
