using DocumentGenerator.Entities.Enums;

namespace DocumentGenerator.Entities.Default;

/// <summary>
/// Класс стандартных ролей пользователя
/// </summary>
public class UserRolesDefault
{
    /// <summary>
    /// Сущности стандартных ролей пользователя
    /// </summary>
    public static List<UserRole> Entities = new List<UserRole>()
    {
        new UserRole()
        {
            Id = DefaultRoleToGuid(Role.Viewer),
            Role = Role.Viewer,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        },
        new UserRole()
        {
            Id = DefaultRoleToGuid(Role.Editor),
            Role = Role.Editor,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        },
        new UserRole()
        {
            Id = DefaultRoleToGuid(Role.Admin),
            Role = Role.Admin,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        },
    };

    /// <summary>
    /// Возвращает стандартный идентификатор роли пользователя
    /// </summary>
    public static Guid DefaultRoleToGuid(Role role)
    {
        switch (role)
        {
            case Role.Viewer:
                return Guid.Parse("59d20b7b-420d-4dba-b8b5-be625764be5b");
            case Role.Editor:
                return Guid.Parse("213064b1-4ee0-40ea-bacf-d60dd358fedc");
            case Role.Admin:
                return Guid.Parse("9af703f7-8fd7-49a7-b87d-a2c4215cfdb5");
            default:
                return Guid.Empty;
        }
    }
}
