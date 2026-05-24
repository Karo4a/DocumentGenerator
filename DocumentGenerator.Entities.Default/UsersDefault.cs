using DocumentGenerator.Common.Helpers;
using DocumentGenerator.Entities.Enums;

namespace DocumentGenerator.Entities.Default;

/// <summary>
/// Класс стандартных пользователей
/// </summary>
public class UsersDefault
{
    /// <summary>
    /// Сущность пользователя администратора
    /// </summary>
    public static User Admin
    {
        get {
            var salt = SecurityHelper.GenerateSalt32();
            return new User()
            {
                Id = Guid.Parse("8abf3cb5-eccd-4799-89d2-3927b19e2f43"),
                Login = "admin",
                PasswordSalt = salt,
                PasswordHash = SecurityHelper.HashPassword32("admin", salt),
                UserRoleId = UserRolesDefault.DefaultRoleToGuid(Role.Admin),
                SecurityStamp = Guid.Parse("9756a2bc-0c7d-47c2-bd9c-e4f5e853a8fc"),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };
        }
    }
}
