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
            return new User()
            {
                Id = Guid.Parse("8abf3cb5-eccd-4799-89d2-3927b19e2f43"),
                Login = "admin",
                PasswordSalt = "SYBZDs4NU+AUGKkcHi+FSA7JkfA+oQLTaa/ppyadW0s=",
                PasswordHash = "uazoiFxmoox9osCYUVxtL/+6+B0UIiUTgC7WCtELKYI=",
                UserRoleId = UserRolesDefault.DefaultRoleToGuid(Role.Admin),
                SecurityStamp = Guid.Parse("9756a2bc-0c7d-47c2-bd9c-e4f5e853a8fc"),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };
        }
    }
}
