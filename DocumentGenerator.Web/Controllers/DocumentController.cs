using AutoMapper;
using DocumentGenerator.Services.Contracts;
using DocumentGenerator.Services.Contracts.IServices;
using DocumentGenerator.Services.Contracts.Models.Document;
using DocumentGenerator.Web.Models.Document;
using DocumentGenerator.Web.Models.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace DocumentGenerator.Web.Controllers
{
    /// <summary>
    /// CRUD контроллер по работе с документами
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentServices service;
        private readonly IExcelServices exportServices;
        private readonly IMapper mapper;
        private readonly IValidateService validateService;

        /// <summary>
        /// Конструктор
        /// </summary>
        public DocumentController(IDocumentServices service,
            IExcelServices exportServices,
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
        /// Получает список всех документов
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<DocumentApiModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAll(CancellationToken cancellationToken)
        {
            var items = await service.GetAll(cancellationToken);
            return Ok(mapper.Map<IReadOnlyCollection<DocumentApiModel>>(items));
        }

        /// <summary>
        /// Добавляет новый документ
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(DocumentApiModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status409Conflict)]
        public async Task<ActionResult> Create(DocumentRequestApiModel request, CancellationToken cancellationToken)
        {
            var requestModel = mapper.Map<DocumentCreateModel>(request);
            await validateService.Validate(requestModel, cancellationToken);
            var result = await service.Create(requestModel, cancellationToken);

            return Ok(mapper.Map<DocumentApiModel>(result));
        }

        /// <summary>
        /// Редактирует документ
        /// </summary>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(IEnumerable<DocumentApiModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Edit([FromRoute] Guid id, [FromBody] DocumentRequestApiModel request, CancellationToken cancellationToken)
        {
            var requestModel = mapper.Map<DocumentCreateModel>(request);
            await validateService.Validate(requestModel, cancellationToken);
            var result = await service.Edit(id, requestModel, cancellationToken);

            return Ok(mapper.Map<DocumentModel>(result));
        }

        /// <summary>
        /// Удаляет документ
        /// </summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            await service.Delete(id, cancellationToken);
            return Ok();
        }
    }
}
