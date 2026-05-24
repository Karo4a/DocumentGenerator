using DocumentGenerator.Common.Mvc.Models;
using DocumentGenerator.Entities;
using DocumentGenerator.Repositories.Contracts.Models;
using DocumentGenerator.Services.Contracts.IServices;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace DocumentGenerator.Services;

/// <inheritdoc cref="ITokenService"/>
public class TokenService : ITokenService, IServiceAnchor
{
    private readonly JwtSettingsModel authSetting;

    /// <summary>
    /// Конструктор
    /// </summary>
    public TokenService(JwtSettingsModel authSetting)
    {
        this.authSetting = authSetting;
    }

    string ITokenService.CreateAccessToken(UserDbModel user)
    {
        var authClaims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, user.Login),
            new(ClaimTypes.Email, user.Email),
            new("SecurityStamp", user.SecurityStamp.ToString()),
            new(ClaimTypes.Role, user.UserRole.Role.ToString())
        };

        var signKey = new SymmetricSecurityKey(
            Base64UrlEncoder.DecodeBytes(authSetting.SignKeyBase64));
        var encKey = new SymmetricSecurityKey(
            Base64UrlEncoder.DecodeBytes(authSetting.SecretKeyBase64));

        var handler = new JwtSecurityTokenHandler();

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = authSetting.Issuer,
            Audience = authSetting.Audience,
            Subject = new ClaimsIdentity(authClaims),
            Expires = DateTime.UtcNow.AddSeconds(authSetting.LifeTimeSec),
            SigningCredentials = new SigningCredentials(signKey, SecurityAlgorithms.HmacSha256),
            EncryptingCredentials = new EncryptingCredentials(
                encKey, SecurityAlgorithms.Aes256KW, SecurityAlgorithms.Aes256CbcHmacSha512)
        };

        return handler.CreateEncodedJwt(tokenDescriptor);
    }

    RefreshToken ITokenService.CreateRefreshToken(UserDbModel user)
    {
        var now = DateTimeOffset.UtcNow;
        return new RefreshToken
        {
            Id = Guid.NewGuid(),
            Expires = now.AddDays(authSetting.RefreshLifeTimeDays),
            UserId = user.Id,
            SecurityStamp = user.SecurityStamp.ToString(),
            CreatedAt = now,
        };
    }
}
