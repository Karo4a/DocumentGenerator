namespace DocumentGenerator.Services.Contracts.Models.Party
{
    /// <summary>
    /// Модель стороны акта
    /// </summary>
    public class PartyModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Полное имя
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Должность
        /// </summary>
        public string Job { get; set; } = string.Empty;

        /// <summary>
        /// ИНН
        /// </summary>
        public string TaxId { get; set; } = string.Empty;
    }
}
