using Microsoft.AspNetCore.Mvc;
using TemperoDaVovo.Application.UseCases.Product.Commands.Create;
using TemperoDaVovo.Application.UseCases.Product.Commands.Delete;
using TemperoDaVovo.Application.UseCases.Product.Commands.Duplicate;
using TemperoDaVovo.Application.UseCases.Product.Commands.ToggleProductActive;
using TemperoDaVovo.Application.UseCases.Product.Commands.Update;
using TemperoDaVovo.Application.UseCases.Product.Commands.UpdateImage;
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
        private readonly IGetAllProductUseCase _getAllProductUseCase;
        private readonly IDeleteProductUseCase _deleteProductUseCase;
        private readonly IToggleProductActiveUseCase _toggleProductActiveUseCase;
        private readonly IUpdateProductUseCase _updateProductUseCase;
        private readonly IGetProductByIdUseCase _getProductByIdUseCase;
        private readonly IUpdateProductImageUseCase _updateProductImageUseCase;
        private readonly IDuplicateProductUseCase _duplicateProductUseCase;

        public ProductsController(ICreateProductUseCase createProductUseCase, IGetAllProductUseCase getAllProductUseCase, IDeleteProductUseCase deleteProductUseCase, IToggleProductActiveUseCase toggleProductActiveUseCase, IUpdateProductUseCase updateProductUseCase, IGetProductByIdUseCase getProductByIdUseCase, IUpdateProductImageUseCase updateProductImageUseCase, IDuplicateProductUseCase duplicateProductUseCase)
        {
            _createProductUseCase = createProductUseCase;
            _getAllProductUseCase = getAllProductUseCase;
            _deleteProductUseCase = deleteProductUseCase;
            _toggleProductActiveUseCase = toggleProductActiveUseCase;
            _updateProductUseCase = updateProductUseCase;
            _getProductByIdUseCase = getProductByIdUseCase;
            _updateProductImageUseCase = updateProductImageUseCase;
            _duplicateProductUseCase = duplicateProductUseCase;
        }

        [HttpPost]
        [ProducesResponseType(typeof(CreateProductResponseJson), StatusCodes.Status201Created)]
        public async Task<IActionResult> Post([FromForm] CreateProductRequestJson product, IFormFile? file)
        {
            var request = await _createProductUseCase.Execute(product, file);
            return Created(string.Empty, request);
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<GetAllProductsResponseJson>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll([FromQuery] Guid restaurantId, string? search)
        {
            var request = await _getAllProductUseCase.Execute(restaurantId, search);
            return Ok(request);
        }

        [HttpDelete("{productId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete([FromRoute] Guid productId)
        {
            await _deleteProductUseCase.Execute(productId);
            return NoContent();
        }

        [HttpPatch("{id}/active")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ToggleActive(Guid id, [FromBody] ToggleProductActiveRequestJson requestJson)
        {
            var response = await _toggleProductActiveUseCase.Execute(id, requestJson.IsActive);
            return Ok(response);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Update(
            [FromRoute] Guid id,
            [FromBody] UpdateProductRequestJson requestJson)
        {
            await _updateProductUseCase.Execute(requestJson, id);
            return NoContent();
        }

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

            await _updateProductImageUseCase.Execute(id, image);
            return NoContent();
        }

        // NOVO: Endpoint para remover a imagem do produto
        /*[HttpDelete("{id}/image")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> RemoveImage([FromRoute] Guid id)
        {
            // Você precisará criar este use case
            // await _removeProductImageUseCase.Execute(id);
            
            // Por enquanto, retorna NotImplemented para você criar o use case depois
            return StatusCode(501, "Endpoint de remoção de imagem ainda não implementado no backend");
        }*/


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var product = await _getProductByIdUseCase.Execute(id);
            return Ok(product);
        }

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