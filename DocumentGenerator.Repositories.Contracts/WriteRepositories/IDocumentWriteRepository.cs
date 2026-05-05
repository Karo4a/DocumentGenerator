using DocumentGenerator.Context.Contracts;
using DocumentGenerator.Entities;

namespace DocumentGenerator.Repositories.Contracts.WriteRepositories
{
    /// <summary>
    /// Репозиторий записи <see cref="Document"/>
    /// </summary>
    public interface IDocumentWriteRepository : IDbWriter<Document>
    {
    }
}
