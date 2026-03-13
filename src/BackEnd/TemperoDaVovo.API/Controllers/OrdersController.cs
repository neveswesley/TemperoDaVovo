using Microsoft.AspNetCore.Mvc;
using TemperoDaVovo.Application.UseCases.Order.Commands.AcceptOrder;
using TemperoDaVovo.Application.UseCases.Order.Commands.AddItemToOrder;
using TemperoDaVovo.Application.UseCases.Order.Commands.Cancel;
using TemperoDaVovo.Application.UseCases.Order.Commands.CompleteCheckout;
using TemperoDaVovo.Application.UseCases.Order.Commands.ExistingPhone;
using TemperoDaVovo.Application.UseCases.Order.Commands.Finalize;
using TemperoDaVovo.Application.UseCases.Order.Commands.RemoveAll;
using TemperoDaVovo.Application.UseCases.Order.Commands.RemoveOrderItem;
using TemperoDaVovo.Application.UseCases.Order.Commands.UpdateOrderItem;
using TemperoDaVovo.Application.UseCases.Order.Queries.CurrentOrder;
using TemperoDaVovo.Application.UseCases.Order.Queries.GetOrderByCliente;
using TemperoDaVovo.Application.UseCases.Order.Queries.GetOrderByRestaurant;
using TemperoDaVovo.Communications.Requests;

namespace TemperoDaVovo.API.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IAddItemToOrderUseCase _addItemToOrderUseCase;
        private readonly IGetCurrentOrderUseCase _getCurrentOrderUseCase;
        private readonly IUpdateOrderItemUseCase _updateOrderItemUseCase;
        private readonly IRemoveOrderItemUseCase _removeOrderItemUseCase;
        private readonly IRemoveAllOrderItemUseCase _removeAllOrderItemUseCase;
        private readonly ICompleteCheckoutUseCase _completeCheckoutUseCase;
        private readonly IExistingPhoneUseCase _existingPhoneUseCase;
        private readonly IFinalizeOrderUseCase _finalizeOrderUseCase;
        private readonly IGetOrderByClientUseCase _getOrderByClientUseCase;
        private readonly ICancelOrderUseCase _cancelOrderUseCase;
        private readonly IGetOrderByRestaurantId _getOrderByRestaurantId;
        private readonly IChangeOrderStatusUseCase _changeOrderStatusUseCase;

        public OrdersController(IAddItemToOrderUseCase addItemToOrderUseCase, IGetCurrentOrderUseCase getCurrentOrderUseCase, IUpdateOrderItemUseCase updateOrderItemUseCase, IRemoveOrderItemUseCase removeOrderItemUseCase, IRemoveAllOrderItemUseCase removeAllOrderItemUseCase, ICompleteCheckoutUseCase completeCheckoutUseCase, IExistingPhoneUseCase existingPhoneUseCase, IFinalizeOrderUseCase finalizeOrderUseCase, IGetOrderByClientUseCase getOrderByClientUseCase, ICancelOrderUseCase cancelOrderUseCase, IGetOrderByRestaurantId getOrderByRestaurantId, IChangeOrderStatusUseCase changeOrderStatusUseCase)
        {
            _addItemToOrderUseCase = addItemToOrderUseCase;
            _getCurrentOrderUseCase = getCurrentOrderUseCase;
            _updateOrderItemUseCase = updateOrderItemUseCase;
            _removeOrderItemUseCase = removeOrderItemUseCase;
            _removeAllOrderItemUseCase = removeAllOrderItemUseCase;
            _completeCheckoutUseCase = completeCheckoutUseCase;
            _existingPhoneUseCase = existingPhoneUseCase;
            _finalizeOrderUseCase = finalizeOrderUseCase;
            _getOrderByClientUseCase = getOrderByClientUseCase;
            _cancelOrderUseCase = cancelOrderUseCase;
            _getOrderByRestaurantId = getOrderByRestaurantId;
            _changeOrderStatusUseCase = changeOrderStatusUseCase;
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
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetCurrent([FromQuery] Guid restaurantId, [FromQuery] string clientSessionId)
        {
            var result = await _getCurrentOrderUseCase.Execute(restaurantId, clientSessionId);
            if (result == null) return NoContent();
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

        [HttpDelete("remove-all-order-item/{orderId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RemoveAllOrderItem([FromRoute] Guid orderId)
        {
            await _removeAllOrderItemUseCase.Execute(orderId);
            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPatch("complete-checkout/{orderId}")]
        public async Task<IActionResult> CompleteCheckout(
            [FromRoute] Guid orderId,
            [FromBody] CompleteCheckoutRequestJson request)
        {
            await _completeCheckoutUseCase.Execute(request);
            return NoContent();
        }
        
        [HttpGet("existing-phone/{phone}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> ExistingPhone([FromRoute] string phone)
        {
            var name = await _existingPhoneUseCase.Execute(phone);
            if (name is null) return NoContent();
            return Ok(new { name });
        }

        [HttpPut("finalize-order/{orderId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> FinalizeOrder([FromRoute] Guid orderId, [FromBody] CheckoutOrderRequestJson request)
        {
            var result = await _finalizeOrderUseCase.ExecuteAsync(request);
            return Ok(result);
        }
        
        [HttpGet("orders/{clientSessionId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCurrent([FromRoute] string clientSessionId)
        {
            var result = await _getOrderByClientUseCase.Execute(clientSessionId);
            return Ok(result);
        }

        [HttpPut("cancel-order/{orderId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CancelOrder([FromRoute] Guid orderId, [FromBody] CancelOrderRequestJson request)
        {
            await _cancelOrderUseCase.ExecuteAsync(orderId, request);
            return NoContent();
        }

        [HttpGet("orders-by-restaurant/{restaurantId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetOrderByRestaurant([FromRoute] Guid restaurantId)
        {
            var result = await _getOrderByRestaurantId.ExecuteAsync(restaurantId);
            return Ok(result);
        }
        
        [HttpPut("change-order-status/{orderId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> ChangeOrderStatus([FromRoute] Guid orderId)
        {
            await _changeOrderStatusUseCase.ExecuteAsync(orderId);
            return NoContent();
        }
        
    }
}
