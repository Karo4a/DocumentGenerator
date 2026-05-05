using DocumentGenerator.Api.Models.Exceptions;
using DocumentGenerator.Api.Models.Product;
using DocumentGenerator.Web.Models;
using System.Net;
using System.Text.Json;

namespace DocumentGenerator.Web.Services.Product
{
    public class ProductService : IProductService
    {
        private readonly HttpClient httpClient;
        private readonly JsonSerializerOptions jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public ProductService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        async Task<Result<ProductApiModel[]>> IProductService.GetAllAsync()
        {
            try
            {
                var response = await httpClient.GetAsync("api/Product");
                return await ProcessApiResponseAsync<ProductApiModel[]>(response);
            }
            catch (Exception ex)
            {
                return Result<ProductApiModel[]>.Failure($"Ошибка сети при получении товаров: {ex.Message}", null, ex.Message);
            }
        }

        async Task<Result<ProductApiModel?>> IProductService.GetByIdAsync(Guid id)
        {
            try
            {
                var response = await httpClient.GetAsync($"api/Product/{id}");
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return Result<ProductApiModel?>.Success(null);
                }
                return await ProcessApiResponseAsync<ProductApiModel?>(response);
            }
            catch (Exception ex)
            {
                return Result<ProductApiModel?>.Failure($"Ошибка сети при получении товара: {ex.Message}", null, ex.Message);
            }
        }

        async Task<Result<ProductApiModel>> IProductService.CreateAsync(ProductRequestApiModel model)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync("api/Product", model, jsonOptions);
                return await ProcessApiResponseAsync<ProductApiModel>(response);
            }
            catch (Exception ex)
            {
                return Result<ProductApiModel>.Failure($"Ошибка сети при создании товара: {ex.Message}", null, ex.Message);
            }
        }

        async Task<Result<ProductApiModel>> IProductService.UpdateAsync(Guid id, ProductRequestApiModel model)
        {
            try
            {
                var response = await httpClient.PutAsJsonAsync($"api/Product/{id}", model, jsonOptions);
                return await ProcessApiResponseAsync<ProductApiModel>(response);
            }
            catch (Exception ex)
            {
                return Result<ProductApiModel>.Failure($"Ошибка сети при обновлении товара: {ex.Message}", null, ex.Message);
            }
        }

        async Task<Result> IProductService.DeleteAsync(Guid id)
        {
            try
            {
                var response = await httpClient.DeleteAsync($"api/Product/{id}");
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return Result.Success();
                }

                if (response.IsSuccessStatusCode)
                {
                    return Result.Success();
                }
                else
                {
                    var errorDetails = await response.Content.ReadAsStringAsync();
                    return Result.Failure(
                        $"Ошибка при удалении товара: {response.StatusCode}",
                        (int)response.StatusCode,
                        errorDetails
                    );
                }
            }
            catch (Exception ex)
            {
                return Result.Failure($"Ошибка сети при удалении товара: {ex.Message}", null, ex.Message);
            }
        }

        private async Task<Result<T>> ProcessApiResponseAsync<T>(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.NoContent)
                {
                    if (typeof(T) != typeof(object) && typeof(T) != typeof(string))
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        if (string.IsNullOrEmpty(content))
                        {
                            return Result<T>.Failure("Ответ API не содержит ожидаемого тела.", (int)response.StatusCode, content);
                        }
                    }
                }
                var json = await response.Content.ReadAsStringAsync();
                var data = JsonSerializer.Deserialize<T>(json, jsonOptions);
                return Result<T>.Success(data!);
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                var errorMessage = FormatErrorMessage(response.StatusCode, errorContent);
                return Result<T>.Failure(errorMessage, (int)response.StatusCode, errorContent);
            }
        }

        private string FormatErrorMessage(HttpStatusCode statusCode, string errorContent)
        {
            try
            {
                if (statusCode == HttpStatusCode.UnprocessableEntity) // 422
                {
                    var validationError = JsonSerializer.Deserialize<ApiValidationExceptionDetail>(errorContent, jsonOptions);
                    if (validationError?.Errors != null && validationError.Errors.Any())
                    {
                        var messages = validationError.Errors.Select(e => $"{e.Field}: {e.Message}").ToList();
                        return $"Ошибка валидации: {string.Join("; ", messages)}";
                    }
                }
                else if (statusCode == HttpStatusCode.Conflict) // 409
                {
                    return "Товар с такими параметрами уже существует.";
                }
                else if (statusCode == HttpStatusCode.NotFound) // 404
                {
                    return "Запрашиваемый ресурс не найден.";
                }
            }
            catch (JsonException)
            {
            }

            return $"Ошибка API: {statusCode} (Код {(int)statusCode}).";
        }
    }
}