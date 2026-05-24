using AutoMapper;
using DocumentGenerator.Common.Helpers;
using DocumentGenerator.Context.Contracts;
using DocumentGenerator.Entities;
using DocumentGenerator.Entities.Enums;
using DocumentGenerator.Repositories.Contracts.ReadRepositories;
using DocumentGenerator.Repositories.Contracts.WriteRepositories;
using DocumentGenerator.Services.Contracts.Exceptions;
using DocumentGenerator.Services.Contracts.IServices;
using DocumentGenerator.Services.Contracts.Models.User;

namespace DocumentGenerator.Services;

/// <inheritdoc cref="IUserService"/>
public class UserService : IUserService, IServiceAnchor
{
    private readonly IUserReadRepository userReadRepository;
    private readonly IUserWriteRepository userWriteRepository;
    private readonly IRefreshTokenReadRepository refreshTokenReadRepository;
    private readonly IRefreshTokenWriteRepository refreshTokenWriteRepository;
    private readonly IUserRoleReadRepository userRoleReadRepository;
    private readonly IMapper mapper;
    private readonly IUnitOfWork unitOfWork;

    /// <summary>
    /// Конструктор
    /// </summary>
    public UserService(
        IUserReadRepository userReadRepository,
        IUserWriteRepository userWriteRepository,
        IRefreshTokenReadRepository refreshTokenReadRepository,
        IRefreshTokenWriteRepository refreshTokenWriteRepository,
        IUserRoleReadRepository userRoleReadRepository,
        IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        this.userReadRepository = userReadRepository;
        this.userWriteRepository = userWriteRepository;
        this.refreshTokenReadRepository = refreshTokenReadRepository;
        this.refreshTokenWriteRepository = refreshTokenWriteRepository;
        this.userRoleReadRepository = userRoleReadRepository;
        this.mapper = mapper;
        this.unitOfWork = unitOfWork;
    }

    async Task<UserModel> IUserService.GetById(Guid id, CancellationToken cancellationToken)
    {
        var user = await userReadRepository.GetById(id, cancellationToken)
            ?? throw new DocumentGeneratorNotFoundException($"Не удалось найти пользователя с идентификатором {id}");
        return mapper.Map<UserModel>(user);
    }

    async Task<IReadOnlyCollection<UserModel>> IUserService.GetAll(CancellationToken cancellationToken)
    {
        var users = await userReadRepository.GetAll(cancellationToken);
        return mapper.Map<List<UserModel>>(users);
    }

    async Task<UserModel> IUserService.Create(UserCreateModel model, CancellationToken cancellationToken)
    {
        if (await userReadRepository.Any(x => x.Login == model.Login, cancellationToken))
        {
            throw new DocumentGeneratorDuplicateException($"Пользователь с логином {model.Login} уже существует.");
        }

        if (await userReadRepository.Any(x => x.Email == model.Email, cancellationToken))
        {
            throw new DocumentGeneratorDuplicateException($"Пользователь с email {model.Email} уже существует.");
        }

        var role = await userRoleReadRepository.GetByRole(Role.Viewer, cancellationToken)
            ?? throw new DocumentGeneratorException("Роль по умолчанию не найдена.");

        var salt = SecurityHelper.GenerateSalt32();

        var user = new User
        {
            Id = Guid.NewGuid(),
            Login = model.Login,
            Email = model.Email,
            PasswordHash = SecurityHelper.HashPassword32(model.Password, salt),
            PasswordSalt = salt,
            UserRoleId = role.Id,
            SecurityStamp = Guid.NewGuid()
        };

        userWriteRepository.Add(user);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return mapper.Map<UserModel>(user);
    }

    async Task<UserModel> IUserService.ChangeRole(Guid id, Contracts.Models.Enums.UserRole role, CancellationToken cancellationToken)
    {
        var userDbModel = await userReadRepository.GetById(id, cancellationToken)
            ?? throw new DocumentGeneratorNotFoundException($"Не удалось найти пользователя с идентификатором {id}");
        var user = mapper.Map<User>(userDbModel);

        var newRole = await userRoleReadRepository.GetByRole(mapper.Map<Role>(role), cancellationToken)
            ?? throw new DocumentGeneratorNotFoundException($"Роль {role} не найдена.");

        user.UserRoleId = newRole.Id;
        user.SecurityStamp = Guid.NewGuid();

        var oldToken = await refreshTokenReadRepository.GetByUserId(user.Id, cancellationToken);
        if (oldToken is not null)
        {
            refreshTokenWriteRepository.Delete(oldToken);
        }

        userWriteRepository.Edit(user);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return mapper.Map<UserModel>(user);
    }

    async Task IUserService.Delete(Guid id, CancellationToken cancellationToken)
    {
        var userDbModel = await userReadRepository.GetById(id, cancellationToken)
            ?? throw new DocumentGeneratorNotFoundException($"Не удалось найти пользователя с идентификатором {id}");
        var user = mapper.Map<User>(userDbModel);

        var oldToken = await refreshTokenReadRepository.GetByUserId(user.Id, cancellationToken);
        if (oldToken != null)
        {
            refreshTokenWriteRepository.Delete(oldToken);
        }

        userWriteRepository.Delete(user);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
