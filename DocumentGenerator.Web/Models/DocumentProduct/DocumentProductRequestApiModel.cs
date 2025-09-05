namespace DocumentGenerator.Web.Models.DocumentProduct
{
    /// <summary>
    /// Модель редактирования документа
    /// </summary>
    /// <param name="ProductId">Идентификатор товара</param>
    /// <param name="Quantity">Количество товара</param>
    /// <param name="Cost">Цена товара на момент создания документа</param>
    public record DocumentProductRequestApiModel(Guid ProductId, int Quantity, decimal Cost);
}