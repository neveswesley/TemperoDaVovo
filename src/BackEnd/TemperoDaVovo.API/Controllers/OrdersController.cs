using Microsoft.AspNetCore.Mvc;
using TemperoDaVovo.Application.UseCases.Order.Commands.AddItemToOrder;
using TemperoDaVovo.Application.UseCases.Order.Commands.RemoveOrderItem;
using TemperoDaVovo.Application.UseCases.Order.Commands.UpdateOrderItem;
using TemperoDaVovo.Application.UseCases.Order.Queries.CurrentOrder;
using TemperoDaVovo.Communications.Requests;

namespace TemperoDaVovo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IAddItemToOrderUseCase _addItemToOrderUseCase;
        private readonly IGetCurrentOrderUseCase _getCurrentOrderUseCase;
        private readonly IUpdateOrderItemUseCase _updateOrderItemUseCase;
        private readonly IRemoveOrderItemUseCase _removeOrderItemUseCase;

        public OrdersController(IAddItemToOrderUseCase addItemToOrderUseCase, IGetCurrentOrderUseCase getCurrentOrderUseCase, IUpdateOrderItemUseCase updateOrderItemUseCase, IRemoveOrderItemUseCase removeOrderItemUseCase)
        {
            _addItemToOrderUseCase = addItemToOrderUseCase;
            _getCurrentOrderUseCase = getCurrentOrderUseCase;
            _updateOrderItemUseCase = updateOrderItemUseCase;
            _removeOrderItemUseCase = removeOrderItemUseCase;
        }

        [HttpPost("add-item")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddItem(AddItemToOrderRequestJson request)
        {
            var result = await _addItemToOrderUseCase.Execute(request);
            return Ok(result);
        }
        
        [HttpGet("current")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCurrent([FromQuery] Guid restaurantId, [FromQuery] string clientSessionId)
        {
            var result = await _getCurrentOrderUseCase.Execute(restaurantId, clientSessionId);
            if (result == null) return NotFound();
            return Ok(result);
        }
        
        [HttpPut("update-order-item/{orderItemId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateOrderItem(
            [FromRoute] Guid orderItemId,
            [FromBody] UpdateOrderItemRequest request,
            CancellationToken ct)
        {
            await _updateOrderItemUseCase.Execute(orderItemId, request, ct);
            return NoContent();
        }

        [HttpDelete("delete-order-item/{orderItemId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteOrderItem([FromRoute] Guid orderItemId)
        {
            await _removeOrderItemUseCase.Execute(orderItemId);
            return NoContent();
        }
    }
}
