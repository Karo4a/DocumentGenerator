using DocumentGenerator.Common;
using DocumentGenerator.Context.Contracts;
using DocumentGenerator.Entities;
using DocumentGenerator.Repositories.Contracts.WriteRepositories;

namespace DocumentGenerator.Repositories.WriteRepositories
{
    /// <inheritdoc cref="IPartyWriteRepository" />
    public class PartyWriteRepository : BaseWriteRepository<Party>, IPartyWriteRepository
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public PartyWriteRepository(IWriter writer, IDateTimeProvider dateTimeProvider)
            : base(writer, dateTimeProvider)
        {

        }
    }
}
