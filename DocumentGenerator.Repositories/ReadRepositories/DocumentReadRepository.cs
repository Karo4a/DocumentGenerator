using DocumentGenerator.Context.Contracts;
using DocumentGenerator.Entities;
using DocumentGenerator.Repositories.Contracts;
using DocumentGenerator.Repositories.Contracts.ReadRepositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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

        Task<bool> IDocumentReadRepository.Any(Expression<Func<Document, bool>> action, CancellationToken cancellationToken)
             => reader.Read<Document>()
                .NotDeletedAt()
                .AnyAsync(action, cancellationToken);

        Task<DocumentDbModel?> IDocumentReadRepository.GetById(Guid id, CancellationToken cancellationToken)
            => reader.Read<Document>()
                .NotDeletedAt()
                .ById(id)
                .Select(x => new DocumentDbModel
                {
                    Id = x.Id,
                    DocumentNumber = x.DocumentNumber,
                    ContractNumber = x.ContractNumber,
                    Date = x.Date,
                    Seller = x.Seller,
                    Buyer = x.Buyer,
                    Products = x.Products
                        .Where(x => x.DeletedAt == null)
                        .Select(x => new DocumentProductDbModel
                        {
                            Id = x.Id,
                            Product = x.Product,
                            Quantity = x.Quantity,
                            Cost = x.Cost,
                        }).ToList(),
                })
                .FirstOrDefaultAsync(cancellationToken);

        Task<IReadOnlyCollection<DocumentDbModel>> IDocumentReadRepository.GetAll(CancellationToken cancellationToken)
            => reader.Read<Document>()
                .NotDeletedAt()
                .Select(x => new DocumentDbModel
                {
                    Id = x.Id,
                    DocumentNumber = x.DocumentNumber,
                    ContractNumber = x.ContractNumber,
                    Date = x.Date,
                    Seller = x.Seller,
                    Buyer = x.Buyer,
                    Products = x.Products
                        .Where(x => x.DeletedAt == null)
                        .Select(x => new DocumentProductDbModel
                        {
                            Id = x.Id,
                            Product = x.Product,
                            Quantity = x.Quantity,
                            Cost = x.Cost,
                        }).ToList(),
                })
                .OrderBy(x => x.DocumentNumber)
                .ToReadOnlyCollectionAsync(cancellationToken);
    }
}
