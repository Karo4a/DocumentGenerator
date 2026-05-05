namespace DocumentGenerator.Common.Contracts
{
    /// <summary>
    /// Интерфейс поставщика ставки НДС
    /// </summary>
    public interface IVatRateProvider
    {
        /// <summary>
        /// Возвращает ставку НДС
        /// </summary>
        public decimal GetVatRate();
    }
}
