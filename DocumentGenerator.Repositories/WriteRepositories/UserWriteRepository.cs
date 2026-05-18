using DocumentGenerator.Common.Contracts;
using DocumentGenerator.Context.Contracts;
using DocumentGenerator.Entities;
using DocumentGenerator.Repositories.Contracts.WriteRepositories;

namespace DocumentGenerator.Repositories.WriteRepositories
{
    /// <inheritdoc cref="IUserWriteRepository" />
    public class UserWriteRepository : BaseWriteRepository<User>,
        IUserWriteRepository,
        IRepositoryAnchor
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public UserWriteRepository(IWriter writer, IDateTimeProvider dateTimeProvider)
            : base(writer, dateTimeProvider)
        {

        }
    }
}
