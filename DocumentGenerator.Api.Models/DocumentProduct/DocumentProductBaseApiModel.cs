namespace DocumentGenerator.Api.Models.DocumentProduct;

/// <summary>
/// Базовая модель товара документа
/// </summary>
public class DocumentProductBaseApiModel
{
    /// <summary>
    /// Количество товара
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Цена товара на момент создания документа
    /// </summary>
    public decimal Cost { get; set; }
}
