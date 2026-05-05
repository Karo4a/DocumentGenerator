using DocumentGenerator.Common.Contracts;
using DocumentGenerator.Context.Contracts;
using DocumentGenerator.Entities;
using DocumentGenerator.Repositories.Contracts.WriteRepositories;

namespace DocumentGenerator.Repositories.WriteRepositories
{
    /// <inheritdoc cref="IDocumentWriteRepository" />
    public class DocumentWriteRepository : BaseWriteRepository<Document>, IDocumentWriteRepository
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public DocumentWriteRepository(IWriter writer, IDateTimeProvider dateTimeProvider)
            : base(writer, dateTimeProvider)
        {

        }
    }
}
