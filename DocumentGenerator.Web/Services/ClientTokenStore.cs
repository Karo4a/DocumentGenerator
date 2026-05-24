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
        try
        {
            var result = await storage.GetAsync<string>(AccessTokenKey);
            return result.Success ? result.Value : null;
        }
        catch (InvalidOperationException)
        {
            return null;
        }
    }

    /// <inheritdoc />
    public async Task<string?> GetRefreshTokenAsync()
    {
        try
        {
            var result = await storage.GetAsync<string>(RefreshTokenKey);
            return result.Success ? result.Value : null;
        }
        catch (InvalidOperationException)
        {
            return null;
        }
    }

    /// <inheritdoc />
    public async Task SetTokensAsync(string accessToken, string refreshToken)
    {
        try
        {
            await storage.SetAsync(AccessTokenKey, accessToken);
            await storage.SetAsync(RefreshTokenKey, refreshToken);
        }
        catch (InvalidOperationException)
        {

        }
    }

    /// <inheritdoc />
    public async Task ClearAsync()
    {
        try
        {
            await storage.DeleteAsync(AccessTokenKey);
            await storage.DeleteAsync(RefreshTokenKey);
        }
        catch (InvalidOperationException)
        {

        }
    }
}
