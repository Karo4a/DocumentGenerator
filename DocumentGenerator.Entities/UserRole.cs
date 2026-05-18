using DocumentGenerator.Entities.Contracts;
using DocumentGenerator.Entities.Enums;

namespace DocumentGenerator.Entities
{
    /// <summary>
    /// Сущность роли пользователя
    /// </summary>
    public class UserRole : DbBaseEntity
    {
        /// <summary>
        /// Название роли
        /// </summary>
        public Role Role { get; set; }
    }
}
