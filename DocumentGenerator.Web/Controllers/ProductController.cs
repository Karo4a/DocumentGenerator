using AutoMapper;
using DocumentGenerator.Services.Contracts;
using DocumentGenerator.Services.Contracts.IServices;
using DocumentGenerator.Services.Contracts.Models.Product;
using DocumentGenerator.Web.Models.Exceptions;
using DocumentGenerator.Web.Models.Product;
using Microsoft.AspNetCore.Mvc;

namespace DocumentGenerator.Web.Controllers
{
    /// <summary>
    /// CRUD контроллер по работе с товарами
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductServices service;
        private readonly IMapper mapper;
        private readonly IValidateService validateService;

        /// <summary>
        /// Конструктор
        /// </summary>
        public ProductController(IProductServices service,
            IMapper mapper,
            IValidateService validateService)
        {
            this.service = service;
            this.mapper = mapper;
            this.validateService = validateService;
        }

        /// <summary>
        /// Получает товар по идентификатору
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(ProductApiModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var item = await service.GetById(id, cancellationToken);
            return Ok(mapper.Map<ProductApiModel>(item));
        }

        /// <summary>
        /// Получает список всех товаров
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ProductApiModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAll(CancellationToken cancellationToken)
        {
            var items = await service.GetAll(cancellationToken);
            return Ok(mapper.Map<IReadOnlyCollection<ProductApiModel>>(items));
        }

        /// <summary>
        /// Добавляет новый товар
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(ProductApiModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult> Create([FromBody] ProductRequestApiModel request, CancellationToken cancellationToken)
        {
            var requestModel = mapper.Map<ProductCreateModel>(request);
            await validateService.Validate(requestModel, cancellationToken);
            var result = await service.Create(requestModel, cancellationToken);

            return Ok(mapper.Map<ProductApiModel>(result));
        }

        /// <summary>
        /// Редактирует товар
        /// </summary>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(IEnumerable<ProductApiModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Edit([FromRoute] Guid id, [FromBody] ProductRequestApiModel  request, CancellationToken cancellationToken)
        {
            var requestModel = mapper.Map<ProductCreateModel>(request);
            await validateService.Validate(requestModel, cancellationToken);
            var result = await service.Edit(id, requestModel, cancellationToken);

            return Ok(mapper.Map<ProductModel>(result));
        }

        /// <summary>
        /// Удаляет товар
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
