using DocumentGenerator.Context.Contracts;
using DocumentGenerator.Entities;

namespace DocumentGenerator.Repositories.Contracts.WriteRepositories
{
    /// <summary>
    /// Репозиторий записи <see cref="DocumentProduct"/>
    /// </summary>
    public interface IDocumentProductWriteRepository : IDbWriter<DocumentProduct>
    {
    }
}
