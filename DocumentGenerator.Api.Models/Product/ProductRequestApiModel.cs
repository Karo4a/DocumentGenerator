namespace DocumentGenerator.Api.Models.Product;

/// <summary>
/// Модель редактирования товара
/// </summary>
public class ProductRequestApiModel
{
    /// <summary>
    /// Название
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Актуальная цена
    /// </summary>
    public decimal Cost { get; set; }
}
