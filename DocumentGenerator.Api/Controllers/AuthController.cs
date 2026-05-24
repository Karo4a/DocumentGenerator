using AutoMapper;
using DocumentGenerator.Api.Models.Auth;
using DocumentGenerator.Api.Models.Exceptions;
using DocumentGenerator.Services.Contracts.IServices;
using DocumentGenerator.Services.Contracts.Models.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DocumentGenerator.Api.Controllers;

/// <summary>
/// Контроллер аутентификации
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService authServices;
    private readonly IMapper mapper;

    /// <summary>
    /// Конструктор
    /// </summary>
    public AuthController(IAuthService authServices, IMapper mapper)
    {
        this.authServices = authServices;
        this.mapper = mapper;
    }

    /// <summary>
    /// Выполняет вход пользователя
    /// </summary>
    [AllowAnonymous]
    [HttpPost("login")]
    [ProducesResponseType(typeof(LoginApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Login([FromBody] LoginRequestApiModel request, CancellationToken cancellationToken)
    {
        var requestModel = mapper.Map<LoginModel>(request);
        var result = await authServices.Login(requestModel, cancellationToken);
        return Ok(mapper.Map<LoginApiResponse>(result));
    }

    /// <summary>
    /// Обновляет access-токен по refresh-токену
    /// </summary>
    [AllowAnonymous]
    [HttpPost("refresh")]
    [ProducesResponseType(typeof(LoginApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequestApiModel request, CancellationToken cancellationToken)
    {
        var requestModel = mapper.Map<RefreshTokenCreateModel>(request);
        var result = await authServices.UpdateRefreshToken(requestModel, cancellationToken);
        return Ok(mapper.Map<LoginApiResponse>(result));
    }

    /// <summary>
    /// Выполняет выход пользователя (отзывает refresh-токен)
    /// </summary>
    [Authorize]
    [HttpPost("logout")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Logout(CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!.Value);
        await authServices.Logout(userId, cancellationToken);
        return NoContent();
    }
}
