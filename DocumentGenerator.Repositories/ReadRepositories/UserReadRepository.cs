using DocumentGenerator.Context.Contracts;
using DocumentGenerator.Entities;
using DocumentGenerator.Repositories.Contracts.Models;
using DocumentGenerator.Repositories.Contracts.ReadRepositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DocumentGenerator.Repositories.ReadRepositories
{
    /// <inheritdoc cref="IUserReadRepository" />
    public class UserReadRepository : IUserReadRepository, IRepositoryAnchor
    {
        private readonly IReader reader;

        /// <summary>
        /// Конструктор
        /// </summary>
        public UserReadRepository(IReader reader)
        {
            this.reader = reader;
        }

        Task<bool> IUserReadRepository.Any(Expression<Func<User, bool>> action, CancellationToken cancellationToken)
             => reader.Read<User>()
                .NotDeletedAt()
                .AnyAsync(action, cancellationToken);

        Task<UserDbModel?> IUserReadRepository.GetById(Guid id, CancellationToken cancellationToken)
            => reader.Read<User>()
                .NotDeletedAt()
                .ById(id)
                .Select(x => new UserDbModel
                {
                    Id = x.Id,
                    Login = x.Login,
                    Email = x.Email,
                    PasswordHash = x.PasswordHash,
                    PasswordSalt = x.PasswordSalt,
                    UserRole = x.UserRole,
                })
                .FirstOrDefaultAsync(cancellationToken);

        Task<IReadOnlyCollection<UserDbModel>> IUserReadRepository.GetAll(CancellationToken cancellationToken)
            => reader.Read<User>()
                .NotDeletedAt()
                .Select(x => new UserDbModel
                {
                    Id = x.Id,
                    Login = x.Login,
                    Email = x.Email,
                    PasswordHash = x.PasswordHash,
                    PasswordSalt = x.PasswordSalt,
                    UserRole = x.UserRole,
                })
                .OrderBy(x => x.Login)
                .ToReadOnlyCollectionAsync(cancellationToken);
    }
}
