using AutoMapper;
using DocumentGenerator.Api.Models.Exceptions;
using DocumentGenerator.Api.Models.Party;
using DocumentGenerator.Services.Contracts;
using DocumentGenerator.Services.Contracts.IServices;
using DocumentGenerator.Services.Contracts.Models.Party;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DocumentGenerator.Api.Controllers;

/// <summary>
/// CRUD контроллер по работе со сторонами актов
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class PartyController : ControllerBase
{
    private readonly IPartyService service;
    private readonly IMapper mapper;
    private readonly IValidateService validateService;

    /// <summary>
    /// Конструктор
    /// </summary>
    public PartyController(IPartyService service,
        IMapper mapper,
        IValidateService validateService)
    {
        this.service = service;
        this.mapper = mapper;
        this.validateService = validateService;
    }

    /// <summary>
    /// Получает сторону акта по идентификатору
    /// </summary>
    [Authorize(Roles = "Viewer,Editor,Admin")]
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(PartyApiModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var item = await service.GetById(id, cancellationToken);
        return Ok(mapper.Map<PartyApiModel>(item));
    }

    /// <summary>
    /// Получает список всех сторон актов
    /// </summary>
    [Authorize(Roles = "Viewer,Editor,Admin")]
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<PartyApiModel>), StatusCodes.Status200OK)]
    public async Task<ActionResult> GetAll(CancellationToken cancellationToken)
    {
        var items = await service.GetAll(cancellationToken);
        return Ok(mapper.Map<IReadOnlyCollection<PartyApiModel>>(items));
    }

    /// <summary>
    /// Добавляет новую сторону акта
    /// </summary>
    [Authorize(Roles = "Editor,Admin")]
    [HttpPost]
    [ProducesResponseType(typeof(PartyApiModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status409Conflict)]
    public async Task<ActionResult> Create([FromBody] PartyRequestApiModel request, CancellationToken cancellationToken)
    {
        var requestModel = mapper.Map<PartyCreateModel>(request);
        await validateService.Validate(requestModel, cancellationToken);
        var result = await service.Create(requestModel, cancellationToken);

        return Ok(mapper.Map<PartyApiModel>(result));
    }

    /// <summary>
    /// Редактирует сторону акта
    /// </summary>
    [Authorize(Roles = "Editor,Admin")]
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(PartyApiModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Edit([FromRoute] Guid id, [FromBody] PartyRequestApiModel request, CancellationToken cancellationToken)
    {
        var requestModel = mapper.Map<PartyCreateModel>(request);
        await validateService.Validate(requestModel, cancellationToken);
        var result = await service.Edit(id, requestModel, cancellationToken);

        return Ok(mapper.Map<PartyApiModel>(result));
    }

    /// <summary>
    /// Удаляет сторону акта
    /// </summary>
    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        await service.Delete(id, cancellationToken);
        return Ok();
    }
}
