using DocumentGenerator.Entities.Contracts;
using DocumentGenerator.Entities.Enums;

namespace DocumentGenerator.Entities
{
    /// <summary>
    /// Сущность пользователя
    /// </summary>
    public class User : DbBaseEntity
    {
        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// Электронная почта
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// Соль
        /// </summary>
        public string Salt { get; set; } = string.Empty;

        /// <summary>
        /// Роль пользователя
        /// </summary>
        public Role Role { get; set; }
    }
}
