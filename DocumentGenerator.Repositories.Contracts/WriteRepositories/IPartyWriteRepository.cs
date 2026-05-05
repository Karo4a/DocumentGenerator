using DocumentGenerator.Context.Contracts;
using DocumentGenerator.Entities;

namespace DocumentGenerator.Repositories.Contracts.WriteRepositories
{
    /// <summary>
    /// Репозиторий записи <see cref="Party"/>
    /// </summary>
    public interface IPartyWriteRepository : IDbWriter<Party>
    {
    }
}
