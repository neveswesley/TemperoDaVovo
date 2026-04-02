using Microsoft.AspNetCore.Mvc;
using TemperoDaVovo.Application.UseCases.SideDish.Commands.CreateSideDish;
using TemperoDaVovo.Application.UseCases.SideDish.Commands.DeleteGroup;
using TemperoDaVovo.Application.UseCases.SideDish.Commands.DeleteSideDish;
using TemperoDaVovo.Application.UseCases.SideDish.Commands.LinkGroup;
using TemperoDaVovo.Application.UseCases.SideDish.Commands.RemoveSideDishGroup;
using TemperoDaVovo.Application.UseCases.SideDish.Commands.ToggleSideDishActive;
using TemperoDaVovo.Application.UseCases.SideDish.Commands.UpdateSideDish;
using TemperoDaVovo.Application.UseCases.SideDish.Commands.UpdateSideDishGroup;
using TemperoDaVovo.Application.UseCases.SideDish.Queries.GetAllProductSideDish;
using TemperoDaVovo.Application.UseCases.SideDish.Queries.GetSideDishGroupsByProduct;
using TemperoDaVovo.Application.UseCases.SideDishGroup.Commands;
using TemperoDaVovo.Application.UseCases.SideDishGroup.Queries.GetAllSideDishGroups;
using TemperoDaVovo.Communications.Requests;
using TemperoDaVovo.Communications.Responses;

namespace TemperoDaVovo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SideDishesController : ControllerBase
    {
        private readonly ICreateSideDishGroupUseCase _createSideDishGroupUseCase;
        private readonly IGetAllSideDishGroupsUseCase _getAllSideDishGroupsUseCase;
        private readonly ICreateSideDishUseCase _createSideDishUseCase;
        private readonly ILinkSideDishSideDishGroupsToProductsToProductUseCase _linkSideDishSideDishGroupsToProductsToProductUseCase;
        private readonly IGetAllSideDishGroupByRestaurant0UseCase _getAllSideDishGroupByRestaurant0UseCase;
        private readonly IGetAllSideDishGroupsByProduct _getAllSideDishGroupsByProduct;
        private readonly IUpdateSideDishGroupUseCase _updateSideDishGroupUseCase;
        private readonly IDeleteSideDishGroupUseCase _deleteSideDishGroupUseCase;
        private readonly IDeleteSideDishUseCase _deleteSideDishUseCase;
        private readonly IRemoveSideDishGroupUseCase _removeSideDishGroupUseCase;
        private readonly IUpdateSideDishUseCase _updateSideDishUseCase;
        private readonly IToggleSideDishActiveUseCase _toggleSideDishActiveUseCase;

        public SideDishesController(ICreateSideDishGroupUseCase createSideDishGroupUseCase, IGetAllSideDishGroupsUseCase getAllSideDishGroupsUseCase, ICreateSideDishUseCase createSideDishUseCase, ILinkSideDishSideDishGroupsToProductsToProductUseCase linkSideDishSideDishGroupsToProductsToProductUseCase, IGetAllSideDishGroupByRestaurant0UseCase getAllSideDishGroupByRestaurant0UseCase, IGetAllSideDishGroupsByProduct getAllSideDishGroupsByProduct, IUpdateSideDishGroupUseCase updateSideDishGroupUseCase, IDeleteSideDishGroupUseCase deleteSideDishGroupUseCase, IDeleteSideDishUseCase deleteSideDishUseCase, IRemoveSideDishGroupUseCase removeSideDishGroupUseCase, IUpdateSideDishUseCase updateSideDishUseCase, IToggleSideDishActiveUseCase toggleSideDishActiveUseCase)
        {
            _createSideDishGroupUseCase = createSideDishGroupUseCase;
            _getAllSideDishGroupsUseCase = getAllSideDishGroupsUseCase;
            _createSideDishUseCase = createSideDishUseCase;
            _linkSideDishSideDishGroupsToProductsToProductUseCase = linkSideDishSideDishGroupsToProductsToProductUseCase;
            _getAllSideDishGroupByRestaurant0UseCase = getAllSideDishGroupByRestaurant0UseCase;
            _getAllSideDishGroupsByProduct = getAllSideDishGroupsByProduct;
            _updateSideDishGroupUseCase = updateSideDishGroupUseCase;
            _deleteSideDishGroupUseCase = deleteSideDishGroupUseCase;
            _deleteSideDishUseCase = deleteSideDishUseCase;
            _removeSideDishGroupUseCase = removeSideDishGroupUseCase;
            _updateSideDishUseCase = updateSideDishUseCase;
            _toggleSideDishActiveUseCase = toggleSideDishActiveUseCase;
        }

        [HttpPost("create-side-dish-group")]
        [ProducesResponseType(typeof(CreateSideDishGroupResponseJson), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateSideDishGroup(CreateSideDishGroupRequestJson request)
        {
            var result = await _createSideDishGroupUseCase.Execute(request);
            return Created(string.Empty, result);
        }

        [HttpGet("products/{restaurantId}/side-dish-groups")]
        [ProducesResponseType(typeof(GetAllSideDishGroupsResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllByRestaurantId([FromRoute] Guid restaurantId)
        {
            var result = await _getAllSideDishGroupsUseCase.Execute(restaurantId);
            return Ok(result);
        }
        
        [HttpGet("get-all-side-dish-groups/{restaurantId}")]
        [ProducesResponseType(typeof(GetAllSideDishGroupsResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllByProductId([FromRoute] Guid restaurantId)
        {
            var result = await _getAllSideDishGroupsUseCase.Execute(restaurantId);
            return Ok(result);
        }

        [HttpPost("create-side-dish")]
        [ProducesResponseType(typeof(SideDishResponseJson), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateSideDish(CreateSideDishRequestJson request)
        {
            var result = await _createSideDishUseCase.Execute(request);
            return Created(string.Empty, result);
        }

        [HttpPost("link-groups")]
        public async Task<IActionResult> LinkGroups(LinkSideDishGroupsToProductRequest request)
        {
            await _linkSideDishSideDishGroupsToProductsToProductUseCase.Execute(request);
            return NoContent();
        }

        [HttpGet("products/get-all-side-dish-groups/{restaurantId}")]
        [ProducesResponseType(typeof(SideDishResponseJson), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllSideDishGroups([FromRoute] Guid restaurantId)
        {
            var result = await _getAllSideDishGroupByRestaurant0UseCase.Execute(restaurantId);
            return Ok(result);
        }

        [HttpGet("products/side-dish-groups/{productId}")]
        [ProducesResponseType(typeof(GetAllSideDishGroupsResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllSideGroupsByProduct([FromRoute] Guid productId)
        {
            var result = await _getAllSideDishGroupsByProduct.Execute(productId);
            return Ok(result);
        }

        [HttpPut("{sideDishGroupId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateSideDishGroup([FromRoute] Guid sideDishGroupId, UpdateSideDishGroupJson request)
        {
            await _updateSideDishGroupUseCase.Execute(request, sideDishGroupId);
            return NoContent();
        }

        [HttpPatch("delete-group/{groupId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteSideDishGroup([FromRoute] Guid groupId)
        {
            await _deleteSideDishGroupUseCase.Execute(groupId);
            return NoContent();
        }

        [HttpDelete("remove-side-dish-groups")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemoveSideDishGroup([FromBody] RemoveSideDishGroupFromProductRequestJson request)
        {
            await _removeSideDishGroupUseCase.Execute(request.ProductId, request.SideDishGroupIds);
            return NoContent();
        }

        [HttpDelete("delete-side-dish/{sideDishId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteSideDish([FromRoute] Guid sideDishId)
        {
            await _deleteSideDishUseCase.Execute(sideDishId);
            return NoContent();
        }

        [HttpPut("update-side-dish/{sideDishId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateSideDish([FromRoute] Guid sideDishId, UpdateSideDishRequestJson request)
        {
            await _updateSideDishUseCase.Execute(sideDishId, request.Name, request.Quantity, request.UnitPrice);
            return NoContent();
        }

        [HttpPatch("active/{sideDishId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ToggleActive([FromRoute] Guid sideDishId,
            ToggleSideDishActiveRequestJson request)
        {
            var result = await _toggleSideDishActiveUseCase.Execute(sideDishId, request.IsActive);
            return Ok(result);
        }
        
    }
}
