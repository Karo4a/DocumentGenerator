using DocumentGenerator.Api.Models.Auth;
using DocumentGenerator.Web.Services;
using System.Net;
using System.Net.Http.Headers;

namespace DocumentGenerator.Web.Infrastructure;

/// <summary>
/// Обработчик HTTP-запросов с автоматической подстановкой JWT-токена и обновлением токена при 401
/// </summary>
public class AuthDelegatingHandler : DelegatingHandler
{
    private readonly IClientTokenStore tokenStore;
    private readonly IHttpClientFactory clientFactory;
    private readonly string apiBaseUrl;

    /// <summary>
    /// Конструктор
    /// </summary>
    public AuthDelegatingHandler(IClientTokenStore tokenStore, IHttpClientFactory clientFactory, string apiBaseUrl)
    {
        this.tokenStore = tokenStore;
        this.clientFactory = clientFactory;
        this.apiBaseUrl = apiBaseUrl;
    }

    /// <summary>
    /// Добавляет Bearer-токен к запросу и обрабатывает 401 через обновление токена с повторным запросом
    /// </summary>
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var token = await tokenStore.GetAccessTokenAsync();
        if (token is not null)
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await base.SendAsync(request, cancellationToken);

        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            var newToken = await TryRefreshAsync(cancellationToken);
            if (newToken is not null)
            {
                var retryRequest = await CloneHttpRequestMessageAsync(request);
                retryRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", newToken);
                return await base.SendAsync(retryRequest, cancellationToken);
            }
        }

        return response;
    }

    private async Task<string?> TryRefreshAsync(CancellationToken cancellationToken)
    {
        var refreshToken = await tokenStore.GetRefreshTokenAsync();
        if (refreshToken is null)
            return null;

        using var client = clientFactory.CreateClient();
        client.BaseAddress = new Uri(apiBaseUrl);

        var request = new RefreshTokenRequestApiModel { RefreshToken = refreshToken };

        HttpResponseMessage response;
        try
        {
            response = await client.PostAsJsonAsync("api/Auth/refresh", request, cancellationToken);
        }
        catch
        {
            await tokenStore.ClearAsync();
            return null;
        }

        if (!response.IsSuccessStatusCode)
        {
            await tokenStore.ClearAsync();
            return null;
        }

        LoginApiResponse? result;
        try
        {
            result = await response.Content.ReadFromJsonAsync<LoginApiResponse>(cancellationToken);
        }
        catch
        {
            await tokenStore.ClearAsync();
            return null;
        }

        if (result is null || string.IsNullOrEmpty(result.Token) || string.IsNullOrEmpty(result.RefreshToken))
        {
            await tokenStore.ClearAsync();
            return null;
        }

        await tokenStore.SetTokensAsync(result.Token, result.RefreshToken);
        return result.Token;
    }

    private static async Task<HttpRequestMessage> CloneHttpRequestMessageAsync(HttpRequestMessage request)
    {
        var clone = new HttpRequestMessage(request.Method, request.RequestUri);

        if (request.Content is not null)
        {
            var contentBytes = await request.Content.ReadAsByteArrayAsync();
            clone.Content = new ByteArrayContent(contentBytes);
            if (request.Content.Headers.ContentType is not null)
                clone.Content.Headers.ContentType = request.Content.Headers.ContentType;
            foreach (var header in request.Content.Headers)
            {
                if (header.Key != "Content-Type")
                    clone.Content.Headers.TryAddWithoutValidation(header.Key, header.Value);
            }
        }

        foreach (var header in request.Headers)
            clone.Headers.TryAddWithoutValidation(header.Key, header.Value);

        return clone;
    }
}
