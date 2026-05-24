using AutoMapper;
using DocumentGenerator.Api.Models.Document;
using DocumentGenerator.Api.Models.Exceptions;
using DocumentGenerator.Services.Contracts;
using DocumentGenerator.Services.Contracts.IServices;
using DocumentGenerator.Services.Contracts.Models.Document;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DocumentGenerator.Api.Controllers;

/// <summary>
/// CRUD контроллер по работе с документами
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class DocumentController : ControllerBase
{
    private readonly IDocumentService service;
    private readonly IExcelService exportServices;
    private readonly IMapper mapper;
    private readonly IValidateService validateService;

    /// <summary>
    /// Конструктор
    /// </summary>
    public DocumentController(IDocumentService service,
        IExcelService exportServices,
        IMapper mapper,
        IValidateService validateService)
    {
        this.service = service;
        this.exportServices = exportServices;
        this.mapper = mapper;
        this.validateService = validateService;
    }

    /// <summary>
    /// Экспортирует документ в формате Excel
    /// </summary>
    [Authorize(Roles = "Viewer,Editor,Admin")]
    [HttpGet("{id:guid}/export")]
    [ProducesResponseType(typeof(FileContentResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ExportExcelById([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var documentModel = await service.GetById(id, cancellationToken);
        var excelDocumentStream = exportServices.Export(documentModel, cancellationToken);
        return File(excelDocumentStream.ToArray(),
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            $"Акт приёма передачи товара №{documentModel.DocumentNumber}.xlsx");
    }

    /// <summary>
    /// Получает документ по идентификатору
    /// </summary>
    [Authorize(Roles = "Viewer,Editor,Admin")]
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(DocumentApiModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var item = await service.GetById(id, cancellationToken);
        return Ok(mapper.Map<DocumentApiModel>(item));
    }

    /// <summary>
    /// Получает список всех документов
    /// </summary>
    [Authorize(Roles = "Viewer,Editor,Admin")]
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<DocumentApiModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var items = await service.GetAll(cancellationToken);
        return Ok(mapper.Map<IReadOnlyCollection<DocumentApiModel>>(items));
    }

    /// <summary>
    /// Добавляет новый документ
    /// </summary>
    [Authorize(Roles = "Editor,Admin")]
    [HttpPost]
    [ProducesResponseType(typeof(DocumentApiModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Create([FromBody] DocumentRequestApiModel request, CancellationToken cancellationToken)
    {
        var requestModel = mapper.Map<DocumentCreateModel>(request);
        await validateService.Validate(requestModel, cancellationToken);
        var result = await service.Create(requestModel, cancellationToken);

        return Ok(mapper.Map<DocumentApiModel>(result));
    }

    /// <summary>
    /// Редактирует документ
    /// </summary>
    [Authorize(Roles = "Editor,Admin")]
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(DocumentApiModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Edit([FromRoute] Guid id, [FromBody] DocumentRequestApiModel request, CancellationToken cancellationToken)
    {
        var requestModel = mapper.Map<DocumentCreateModel>(request);
        await validateService.Validate(requestModel, cancellationToken);
        var result = await service.Edit(id, requestModel, cancellationToken);

        return Ok(mapper.Map<DocumentApiModel>(result));
    }

    /// <summary>
    /// Удаляет документ
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
