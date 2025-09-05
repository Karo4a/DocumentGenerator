using DocumentGenerator.Common.Contracts;
using DocumentGenerator.Context.Contracts;
using DocumentGenerator.Entities;
using DocumentGenerator.Repositories.Contracts.WriteRepositories;

namespace DocumentGenerator.Repositories.WriteRepositories
{
    /// <inheritdoc cref="IDocumentProductWriteRepository" />
    public class DocumentProductWriteRepository : BaseWriteRepository<DocumentProduct>, IDocumentProductWriteRepository
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public DocumentProductWriteRepository(IWriter writer, IDateTimeProvider dateTimeProvider)
            : base(writer, dateTimeProvider)
        {

        }
    }
}
