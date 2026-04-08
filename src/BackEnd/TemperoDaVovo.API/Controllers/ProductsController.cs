using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TemperoDaVovo.Application.UseCases.Product.Commands.Create;
using TemperoDaVovo.Application.UseCases.Product.Commands.Delete;
using TemperoDaVovo.Application.UseCases.Product.Commands.Duplicate;
using TemperoDaVovo.Application.UseCases.Product.Commands.RemoveImage;
using TemperoDaVovo.Application.UseCases.Product.Commands.ToggleProductActive;
using TemperoDaVovo.Application.UseCases.Product.Commands.Update;
using TemperoDaVovo.Application.UseCases.Product.Commands.UpdateProductImage;
using TemperoDaVovo.Application.UseCases.Product.Queries.GetAll;
using TemperoDaVovo.Application.UseCases.Product.Queries.GetById;
using TemperoDaVovo.Communications.Requests;
using TemperoDaVovo.Communications.Responses;

namespace TemperoDaVovo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ICreateProductUseCase _createProductUseCase;
        private readonly IGetProductWithSideDishesUseCase _getProductWithSideDishesUseCase;
        private readonly IDeleteProductUseCase _deleteProductUseCase;
        private readonly IToggleProductActiveUseCase _toggleProductActiveUseCase;
        private readonly IUpdateProductUseCase _updateProductUseCase;
        private readonly IGetProductByIdUseCase _getProductByIdUseCase;
        private readonly IUpdateProductImageUseCase _updateProductImageUseCase;
        private readonly IDuplicateProductUseCase _duplicateProductUseCase;
        private readonly IRemoveProductImageUseCase _removeProductImageUseCase;

        public ProductsController(ICreateProductUseCase createProductUseCase, IGetProductWithSideDishesUseCase getProductWithSideDishesUseCase, IDeleteProductUseCase deleteProductUseCase, IToggleProductActiveUseCase toggleProductActiveUseCase, IUpdateProductUseCase updateProductUseCase, IGetProductByIdUseCase getProductByIdUseCase, IUpdateProductImageUseCase updateProductImageUseCase, IDuplicateProductUseCase duplicateProductUseCase, IRemoveProductImageUseCase removeProductImageUseCase)
        {
            _createProductUseCase = createProductUseCase;
            _getProductWithSideDishesUseCase = getProductWithSideDishesUseCase;
            _deleteProductUseCase = deleteProductUseCase;
            _toggleProductActiveUseCase = toggleProductActiveUseCase;
            _updateProductUseCase = updateProductUseCase;
            _getProductByIdUseCase = getProductByIdUseCase;
            _updateProductImageUseCase = updateProductImageUseCase;
            _duplicateProductUseCase = duplicateProductUseCase;
            _removeProductImageUseCase = removeProductImageUseCase;
        }

        [Authorize(Roles = "Restaurant")]
        [HttpPost]
        [ProducesResponseType(typeof(CreateProductResponseJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(CreateProductResponseJson), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(CreateProductResponseJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromForm] CreateProductRequestJson product, IFormFile? file)
        {
            var request = await _createProductUseCase.ExecuteAsync(product, file);
            return Created(string.Empty, request);
        }

        
        [HttpGet]
        [ProducesResponseType(typeof(List<GetProductWithSideDishesResponseJson>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll([FromQuery] Guid restaurantId, string? search)
        {
            var request = await _getProductWithSideDishesUseCase.ExecuteAsync(restaurantId, search);
            return Ok(request);
        }

        [Authorize(Roles = "Restaurant")]
        [HttpPatch("delete-product/{productId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete([FromRoute] Guid productId)
        {
            await _deleteProductUseCase.ExecuteAsync(productId);
            return NoContent();
        }

        [Authorize(Roles = "Restaurant")]
        [HttpPatch("{id}/active")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ToggleActive(Guid id, [FromBody] ToggleProductActiveRequestJson requestJson)
        {
            var response = await _toggleProductActiveUseCase.ExecuteAsync(id, requestJson.IsActive);
            return Ok(response);
        }

        [Authorize(Roles = "Restaurant")]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Update(
            [FromRoute] Guid id,
            [FromBody] UpdateProductRequestJson requestJson)
        {
            await _updateProductUseCase.ExecuteAsync(requestJson, id);
            return NoContent();
        }

        [Authorize(Roles = "Restaurant")]
        [HttpPut("{id}/image")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UpdateImage(
            [FromRoute] Guid id,
            [FromForm] UpdateProductImageRequestJson requestJson
        )
        {
            var image = requestJson.Image;

            if (image == null || image.Length == 0)
                return BadRequest("Imagem inválida");

            await _updateProductImageUseCase.ExecuteAsync(id, image);
            return NoContent();
        }
        
        [Authorize(Roles = "Restaurant")]
        [HttpDelete("{id}/image")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemoveImage([FromRoute] Guid id)
        {
            await _removeProductImageUseCase.ExecuteAsync(id);
            return NoContent();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var product = await _getProductByIdUseCase.ExecuteAsync(id);
            return Ok(product);
        }

        [Authorize(Roles = "Restaurant")]
        [HttpPost("duplicate-product")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Duplicate([FromBody] DuplicateProductRequestJson request)
        {
            var product = await _duplicateProductUseCase.ExecuteAsync(request);
            return Created(string.Empty, product);
        }
    }
}