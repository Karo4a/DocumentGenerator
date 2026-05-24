using AutoMapper;
using DocumentGenerator.Api.Models.Enums;
using DocumentGenerator.Api.Models.Exceptions;
using DocumentGenerator.Api.Models.User;
using DocumentGenerator.Services.Contracts.IServices;
using DocumentGenerator.Services.Contracts.Models.Enums;
using DocumentGenerator.Services.Contracts.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DocumentGenerator.Api.Controllers;

/// <summary>
/// CRUD контроллер по работе с пользователями
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService userService;
    private readonly IMapper mapper;

    /// <summary>
    /// Конструктор
    /// </summary>
    public UserController(IUserService userServices, IMapper mapper)
    {
        this.userService = userServices;
        this.mapper = mapper;
    }

    /// <summary>
    /// Получает пользователя по идентификатору
    /// </summary>
    [Authorize(Roles = "Admin")]
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(UserApiModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var item = await userService.GetById(id, cancellationToken);
        return Ok(mapper.Map<UserApiModel>(item));
    }

    /// <summary>
    /// Получает всех пользователей
    /// </summary>
    [Authorize(Roles = "Admin")]
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<UserApiModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var items = await userService.GetAll(cancellationToken);
        return Ok(mapper.Map<IReadOnlyCollection<UserApiModel>>(items));
    }

    /// <summary>
    /// Создаёт нового пользователя
    /// </summary>
    [AllowAnonymous]
    [HttpPost("register")]
    [ProducesResponseType(typeof(UserApiModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status422UnprocessableEntity)]
    [SwaggerOperation(OperationId = "UserCreate")]
    public async Task<IActionResult> Create([FromBody] UserRequestApiModel request, CancellationToken cancellationToken)
    {
        var requestModel = mapper.Map<UserCreateModel>(request);
        var result = await userService.Create(requestModel, cancellationToken);
        return Ok(mapper.Map<UserApiModel>(result));
    }

    /// <summary>
    /// Изменяет роль пользователя
    /// </summary>
    [Authorize(Roles = "Admin")]
    [HttpPut("{id:guid}/change-role")]
    [ProducesResponseType(typeof(UserApiModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
    [SwaggerOperation(OperationId = "UserChangeRole")]
    public async Task<IActionResult> ChangeRole([FromRoute] Guid id, [FromBody] UserRoleRequestApiModel request, CancellationToken cancellationToken)
    {
        var result = await userService.ChangeRole(id, mapper.Map<UserRole>(request.Role), cancellationToken);
        return Ok(mapper.Map<UserApiModel>(result));
    }

    /// <summary>
    /// Удаляет пользователя
    /// </summary>
    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        await userService.Delete(id, cancellationToken);
        return Ok();
    }

    /// <summary>
    /// Возвращает информацию о текущем авторизованном пользователе
    /// </summary>
    [Authorize]
    [HttpGet("me")]
    [ProducesResponseType(typeof(UserInfoApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status401Unauthorized)]
    [SwaggerOperation(OperationId = "UserGetCurrentInfo")]
    public IActionResult GetCurrentUserInfo()
    {
        var id = Guid.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!.Value);
        var login = User.FindFirst(System.Security.Claims.ClaimTypes.Name)!.Value;
        var email = User.FindFirst(System.Security.Claims.ClaimTypes.Email)!.Value;
        var role = User.FindFirst(System.Security.Claims.ClaimTypes.Role)!.Value;

        return Ok(new UserInfoApiResponse
        {
            Id = id,
            Login = login,
            Email = email,
            Role = Enum.Parse<UserRoleApi>(role)
        });
    }
}
