using Microsoft.AspNetCore.Mvc;
using TemperoDaVovo.Application.UseCases.Category.Commands;
using TemperoDaVovo.Application.UseCases.Category.Commands.Delete;
using TemperoDaVovo.Application.UseCases.Category.Commands.Reorder;
using TemperoDaVovo.Application.UseCases.Category.Commands.UpdateProduct;
using TemperoDaVovo.Application.UseCases.Category.Queries.GetCategoriesWithProducts;
using TemperoDaVovo.Communications.Requests;
using TemperoDaVovo.Communications.Responses;

namespace TemperoDaVovo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        public CategoriesController(ICreateCategoryUseCase createCategoryUseCase, IGetCategoryWithProductsUseCase getCategoryWithProductsUseCase, IUpdateCategoryUseCase updateCategoryUseCase, IDeleteCategoryUseCase deleteCategoryUseCase, IReorderCategoriesUseCase reorderCategoriesUseCase)
        {
            _createCategoryUseCase = createCategoryUseCase;
            _getCategoryWithProductsUseCase = getCategoryWithProductsUseCase;
            _updateCategoryUseCase = updateCategoryUseCase;
            _deleteCategoryUseCase = deleteCategoryUseCase;
            _reorderCategoriesUseCase = reorderCategoriesUseCase;
        }

        private readonly ICreateCategoryUseCase _createCategoryUseCase;
        private readonly IGetCategoryWithProductsUseCase _getCategoryWithProductsUseCase;
        private readonly IUpdateCategoryUseCase _updateCategoryUseCase;
        private readonly IDeleteCategoryUseCase _deleteCategoryUseCase;
        private readonly IReorderCategoriesUseCase _reorderCategoriesUseCase;
        
        [HttpPost]
        [ProducesResponseType(typeof(CreateCategoryResponseJson), StatusCodes.Status201Created)]
        public async Task<IActionResult> Post([FromBody] CreateCategoryRequestJson request, [FromHeader(Name = "X-Restaurant-Id")] Guid restaurantId)
        {
            var response = await _createCategoryUseCase.Execute(request, restaurantId);
            return Created(string.Empty, response);
        }

        [HttpGet("with-products/{restaurantId}")]
        [ProducesResponseType(typeof(CategoryWithProductsResponseJson), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(Guid restaurantId)
        {
            var response = await _getCategoryWithProductsUseCase.Execute(restaurantId);
            return Ok(response);
        }

        [HttpPut("with-products/{categoryId}")]
        [ProducesResponseType(typeof(UpdateCategoryResponseJson), StatusCodes.Status200OK)]
        public async Task<IActionResult> Put([FromBody] UpdateCategoryRequestJson request, [FromRoute] Guid categoryId)
        {
            var response = await _updateCategoryUseCase.Execute(request, categoryId);
            return Ok(response);
        }

        [HttpDelete("{categoryId}")]
        public async Task<IActionResult> Delete(Guid categoryId)
        {
            await _deleteCategoryUseCase.Execute(categoryId);
            return NoContent();
        }

        [HttpPut("reorder-category")]
        [ProducesResponseType(typeof(ReorderCategoriesResponseJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ReorderCategoriesResponseJson), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ReorderCategory(ReorderCategoriesRequest request)
        {
            var result = await _reorderCategoriesUseCase.ExecuteAsync(request);
            return Ok(result);
        }
    }
}
