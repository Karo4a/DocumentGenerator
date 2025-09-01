using AutoMapper;
using DocumentGenerator.Services;
using DocumentGenerator.Services.Contracts;
using DocumentGenerator.Services.Contracts.Models;
using DocumentGenerator.Web.Models;
using DocumentGenerator.Web.Models.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace DocumentGenerator.Web.Controllers
{
    /// <summary>
    /// CRUD контроллер по работе с товарами
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductServices service;
        private readonly IMapper mapper;
        private readonly IValidateService validateService;

        /// <summary>
        /// Конструктор
        /// </summary>
        public ProductsController(IProductServices service,
            IMapper mapper,
            IValidateService validateService)
        {
            this.service = service;
            this.mapper = mapper;
            this.validateService = validateService;
        }

        /// <summary>
        /// Получает список всех товаров
        /// </summary>
        /// GET: /api/products/
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
        /// POST: /api/products/
        [HttpPost]
        [ProducesResponseType(typeof(ProductApiModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult> Create(ProductRequestApiModel request, CancellationToken cancellationToken)
        {
            var requestModel = mapper.Map<ProductCreateModel>(request);
            await validateService.Validate(requestModel, cancellationToken);
            var result = await service.Create(requestModel, cancellationToken);

            return Ok(mapper.Map<ProductApiModel>(result));
        }

        /// <summary>
        /// Редактирует товар
        /// </summary>
        /// PUT: /api/products/c2331ea8-a98d-4c3e-baea-d88f5665947
        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(IEnumerable<ProductApiModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Edit([FromRoute] Guid id, [FromBody] ProductRequestApiModel  request, CancellationToken cancellationToken)
        {
            var requestModel = mapper.Map<ProductModel>(request);
            requestModel.Id = id;

            var result = await service.Edit(requestModel, cancellationToken);

            return Ok(mapper.Map<ProductModel>(result));
        }
    }
}
