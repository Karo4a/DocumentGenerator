using DocumentGenerator.Web.Models.DocumentProduct;

namespace DocumentGenerator.Web.Models.Document
{
    /// <summary>
    /// Модель редактирования документа
    /// </summary>
    /// <param name="DocumentNumber">Номер документа</param>
    /// <param name="ContractNumber">Номер основного договора</param>
    /// <param name="Date">Дата подписания документа</param>
    /// <param name="SellerId">Идентификатор продавца</param>
    /// <param name="BuyerId">Идентификатор покупателя</param>
    /// <param name="Products">Список товаров для документа <see cref="DocumentProductRequestApiModel"/></param>
    public record DocumentRequestApiModel(
        string DocumentNumber,
        string ContractNumber,
        DateOnly Date,
        Guid SellerId,
        Guid BuyerId,
        ICollection<DocumentProductRequestApiModel> Products);
}
