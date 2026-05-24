using DocumentGenerator.Common.Helpers;
using DocumentGenerator.Context.Contracts;
using DocumentGenerator.Repositories.Contracts.ReadRepositories;
using DocumentGenerator.Repositories.Contracts.WriteRepositories;
using DocumentGenerator.Services.Contracts.Exceptions;
using DocumentGenerator.Services.Contracts.IServices;
using DocumentGenerator.Services.Contracts.Models.Auth;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.IdentityModel.Tokens;

namespace DocumentGenerator.Services;

/// <inheritdoc cref="IAuthService"/>
public class AuthService : IAuthService, IServiceAnchor
{
    private readonly IUserReadRepository userReadRepository;
    private readonly IRefreshTokenReadRepository refreshTokenReadRepository;
    private readonly IRefreshTokenWriteRepository refreshTokenWriteRepository;
    private readonly IUnitOfWork unitOfWork;
    private readonly ITokenService tokenService;
    private readonly IDataProtector protector;

    /// <summary>
    /// Конструктор
    /// </summary>
    public AuthService(
        IUserReadRepository userReadRepository,
        IRefreshTokenReadRepository refreshTokenReadRepository,
        IRefreshTokenWriteRepository refreshTokenWriteRepository,
        IUnitOfWork unitOfWork,
        ITokenService tokenService,
        IDataProtectionProvider protectionProvider)
    {
        this.userReadRepository = userReadRepository;
        this.refreshTokenReadRepository = refreshTokenReadRepository;
        this.refreshTokenWriteRepository = refreshTokenWriteRepository;
        this.unitOfWork = unitOfWork;
        this.tokenService = tokenService;
        protector = protectionProvider.CreateProtector(nameof(AuthService));
    }

    async Task<LoginResponseModel> IAuthService.Login(LoginModel model, CancellationToken cancellationToken)
    {
        var user = await userReadRepository.GetByLogin(model.Login, cancellationToken);

        if (user is null || SecurityHelper.HashPassword32(model.Password, user.PasswordSalt) != user.PasswordHash)
            throw new DocumentGeneratorException("Неверный логин или пароль.");

        var oldToken = await refreshTokenReadRepository.GetByUserId(user.Id, cancellationToken);
        if (oldToken != null)
        {
            refreshTokenWriteRepository.Delete(oldToken);
        }

        var accessToken = tokenService.CreateAccessToken(user);
        var refreshToken = tokenService.CreateRefreshToken(user);
        refreshTokenWriteRepository.Add(refreshToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        var protectedToken = Base64UrlEncoder.Encode(
            protector.Protect(refreshToken.Id.ToByteArray()));

        return new LoginResponseModel
        {
            Token = accessToken,
            RefreshToken = protectedToken
        };
    }

    async Task<LoginResponseModel> IAuthService.UpdateRefreshToken(RefreshTokenCreateModel model, CancellationToken cancellationToken)
    {
        var tokenId = new Guid(protector.Unprotect(
            Base64UrlEncoder.DecodeBytes(model.RefreshToken)));

        var storedToken = await refreshTokenReadRepository.GetById(tokenId, cancellationToken)
            ?? throw new DocumentGeneratorNotFoundException("Токен обновления не найден.");

        if (storedToken.Expires <= DateTimeOffset.UtcNow)
            throw new DocumentGeneratorException("Срок действия токена обновления истёк.");

        var user = await userReadRepository.GetById(storedToken.UserId, cancellationToken)
            ?? throw new DocumentGeneratorNotFoundException("Пользователь не найден.");

        if (user.SecurityStamp.ToString() != storedToken.SecurityStamp)
            throw new DocumentGeneratorException("Токен обновления был отозван.");

        refreshTokenWriteRepository.Delete(storedToken);

        var accessToken = tokenService.CreateAccessToken(user);
        var newRefreshToken = tokenService.CreateRefreshToken(user);
        refreshTokenWriteRepository.Add(newRefreshToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        var protectedToken = Base64UrlEncoder.Encode(
            protector.Protect(newRefreshToken.Id.ToByteArray()));

        return new LoginResponseModel
        {
            Token = accessToken,
            RefreshToken = protectedToken
        };
    }

    async Task IAuthService.Logout(Guid userId, CancellationToken cancellationToken)
    {
        var token = await refreshTokenReadRepository.GetByUserId(userId, cancellationToken);
        if (token != null)
        {
            refreshTokenWriteRepository.Delete(token);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
