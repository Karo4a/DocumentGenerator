namespace DocumentGenerator.Web.Models.Document
{
    /// <summary>
    /// Базовая модель документа
    /// </summary>
    public class DocumentBaseApiModel
    {
        /// <summary>
        /// Номер документа
        /// </summary>
        public string DocumentNumber { get; set; } = string.Empty;

        /// <summary>
        /// Номер основного договора
        /// </summary>
        public string ContractNumber { get; set; } = string.Empty;

        /// <summary>
        /// Дата подписания документа
        /// </summary>
        public DateOnly Date { get; set; }
    }
}
