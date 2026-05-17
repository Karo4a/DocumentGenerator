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
        /// Логин пользователя
        /// </summary>
        public string Login { get; set; } = string.Empty;

        /// <summary>
        /// Электронная почта
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Хэш пароля
        /// </summary>
        public string PasswordHash { get; set; } = string.Empty;

        /// <summary>
        /// Соль пароля
        /// </summary>
        public string PasswordSalt { get; set; } = string.Empty;

        /// <summary>
        /// Роль пользователя
        /// </summary>
        public Role Role { get; set; }
    }
}
