using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace DocumentGenerator.Web.Services;

/// <inheritdoc cref="IClientTokenStore"/>
public class ClientTokenStore : IClientTokenStore
{
    private const string AccessTokenKey = "dg_access_token";
    private const string RefreshTokenKey = "dg_refresh_token";

    private readonly ProtectedLocalStorage storage;

    /// <summary>
    /// Конструктор
    /// </summary>
    public ClientTokenStore(ProtectedLocalStorage storage)
    {
        this.storage = storage;
    }

    /// <inheritdoc />
    public async Task<string?> GetAccessTokenAsync()
    {
        var result = await storage.GetAsync<string>(AccessTokenKey);
        return result.Success ? result.Value : null;
    }

    /// <inheritdoc />
    public async Task<string?> GetRefreshTokenAsync()
    {
        var result = await storage.GetAsync<string>(RefreshTokenKey);
        return result.Success ? result.Value : null;
    }

    /// <inheritdoc />
    public async Task SetTokensAsync(string accessToken, string refreshToken)
    {
        await storage.SetAsync(AccessTokenKey, accessToken);
        await storage.SetAsync(RefreshTokenKey, refreshToken);
    }

    /// <inheritdoc />
    public async Task ClearAsync()
    {
        await storage.DeleteAsync(AccessTokenKey);
        await storage.DeleteAsync(RefreshTokenKey);
    }
}
