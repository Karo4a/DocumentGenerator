namespace DocumentGenerator.Api.Models.Party
{
    /// <summary>
    /// Модель стороны акта
    /// </summary>
    public class PartyApiModel : PartyRequestApiModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }
    }
}