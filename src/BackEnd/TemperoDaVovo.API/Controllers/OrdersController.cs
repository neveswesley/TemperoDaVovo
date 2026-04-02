using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TemperoDaVovo.Application.UseCases.Order.Commands.AbandonOrder;
using TemperoDaVovo.Application.UseCases.Order.Commands.AddItemToOrder;
using TemperoDaVovo.Application.UseCases.Order.Commands.ApproveCancellationRequest;
using TemperoDaVovo.Application.UseCases.Order.Commands.Cancel;
using TemperoDaVovo.Application.UseCases.Order.Commands.CancelByRestaurant;
using TemperoDaVovo.Application.UseCases.Order.Commands.CancelOrder;
using TemperoDaVovo.Application.UseCases.Order.Commands.ChangeOrderStatus;
using TemperoDaVovo.Application.UseCases.Order.Commands.CompleteCheckout;
using TemperoDaVovo.Application.UseCases.Order.Commands.ExistingPhone;
using TemperoDaVovo.Application.UseCases.Order.Commands.Finalize;
using TemperoDaVovo.Application.UseCases.Order.Commands.MarkAsDelivered;
using TemperoDaVovo.Application.UseCases.Order.Commands.RejectCancellationRequest;
using TemperoDaVovo.Application.UseCases.Order.Commands.RejectOrder;
using TemperoDaVovo.Application.UseCases.Order.Commands.RemoveAll;
using TemperoDaVovo.Application.UseCases.Order.Commands.RemoveOrderItem;
using TemperoDaVovo.Application.UseCases.Order.Commands.UpdateOrderItem;
using TemperoDaVovo.Application.UseCases.Order.Queries.CurrentOrder;
using TemperoDaVovo.Application.UseCases.Order.Queries.GetOrderByCliente;
using TemperoDaVovo.Application.UseCases.Order.Queries.GetOrderByRestaurant;
using TemperoDaVovo.Application.UseCases.Order.Queries.GetOrderHistory;
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
        private readonly ICancelOrderRequestUseCase _cancelOrderRequestUseCase;
        private readonly IGetOrderByRestaurantId _getOrderByRestaurantId;
        private readonly IChangeOrderStatusUseCase _changeOrderStatusUseCase;
        private readonly IGetOrderHistoryUseCase _getOrderHistoryUseCase;
        private readonly IMarkAsDeliveredUseCase _markAsDeliveredUseCase;
        private readonly IAbandonOrderUseCase _abandonOrderUseCase;
        private readonly IApproveCancellationRequestUseCase _approveCancellationRequestUseCase;
        private readonly IRejectCancellationRequestUseCase _rejectCancellationRequestUseCase;
        private readonly IRejectOrderUseCase _rejectOrderUseCase;
        private readonly ICancelOrderUseCase _cancelOrderUseCase;

        public OrdersController(IAddItemToOrderUseCase addItemToOrderUseCase, IGetCurrentOrderUseCase getCurrentOrderUseCase, IUpdateOrderItemUseCase updateOrderItemUseCase, IRemoveOrderItemUseCase removeOrderItemUseCase, IRemoveAllOrderItemUseCase removeAllOrderItemUseCase, ICompleteCheckoutUseCase completeCheckoutUseCase, IExistingPhoneUseCase existingPhoneUseCase, IFinalizeOrderUseCase finalizeOrderUseCase, IGetOrderByClientUseCase getOrderByClientUseCase, ICancelOrderRequestUseCase cancelOrderRequestUseCase, IGetOrderByRestaurantId getOrderByRestaurantId, IChangeOrderStatusUseCase changeOrderStatusUseCase, IGetOrderHistoryUseCase getOrderHistoryUseCase, IMarkAsDeliveredUseCase markAsDeliveredUseCase, IAbandonOrderUseCase abandonOrderUseCase, IApproveCancellationRequestUseCase approveCancellationRequestUseCase, IRejectCancellationRequestUseCase rejectCancellationRequestUseCase, IRejectOrderUseCase rejectOrderUseCase, ICancelOrderUseCase cancelOrderUseCase)
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
            _cancelOrderRequestUseCase = cancelOrderRequestUseCase;
            _getOrderByRestaurantId = getOrderByRestaurantId;
            _changeOrderStatusUseCase = changeOrderStatusUseCase;
            _getOrderHistoryUseCase = getOrderHistoryUseCase;
            _markAsDeliveredUseCase = markAsDeliveredUseCase;
            _abandonOrderUseCase = abandonOrderUseCase;
            _approveCancellationRequestUseCase = approveCancellationRequestUseCase;
            _rejectCancellationRequestUseCase = rejectCancellationRequestUseCase;
            _rejectOrderUseCase = rejectOrderUseCase;
            _cancelOrderUseCase = cancelOrderUseCase;
        }

        [HttpPost("add-item")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddItem(AddItemToOrderRequestJson request)
        {
            var result = await _addItemToOrderUseCase.ExecuteAsync(request);
            return Ok(result);
        }
        
        [HttpGet("current")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetCurrent([FromQuery] Guid restaurantId, [FromQuery] string clientSessionId)
        {
            var result = await _getCurrentOrderUseCase.ExecuteAsync(restaurantId, clientSessionId);
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
            await _updateOrderItemUseCase.ExecuteAsync(orderItemId, request, ct);
            return NoContent();
        }

        [HttpDelete("delete-order-item/{orderItemId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteOrderItem([FromRoute] Guid orderItemId)
        {
            await _removeOrderItemUseCase.ExecuteAsync(orderItemId);
            return NoContent();
        }

        [HttpDelete("remove-all-order-item/{orderId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RemoveAllOrderItem([FromRoute] Guid orderId)
        {
            await _removeAllOrderItemUseCase.ExecuteAsync(orderId);
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
            await _completeCheckoutUseCase.ExecuteAsync(request);
            return NoContent();
        }
        
        [HttpGet("existing-phone/{phone}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> ExistingPhone([FromRoute] string phone)
        {
            var name = await _existingPhoneUseCase.ExecuteAsync(phone);
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
        public async Task<IActionResult> GetOrder([FromRoute] string clientSessionId)
        {
            var result = await _getOrderByClientUseCase.Execute(clientSessionId);
            return Ok(result);
        }

        [HttpPut("cancel-order/{orderId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CancelOrder([FromRoute] Guid orderId, [FromBody] CancelOrderByCustomerRequestJson byCustomerRequest)
        {
            await _cancelOrderRequestUseCase.ExecuteAsync(orderId, byCustomerRequest);
            return NoContent();
        }

        [Authorize(Roles = "Restaurant")]
        [HttpGet("orders-by-restaurant/{restaurantId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetOrderByRestaurant([FromRoute] Guid restaurantId)
        {
            var result = await _getOrderByRestaurantId.ExecuteAsync(restaurantId);
            return Ok(result);
        }
        
        [Authorize(Roles = "Restaurant")]
        [HttpPut("change-order-status/{orderId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> ChangeOrderStatus([FromRoute] Guid orderId)
        {
            await _changeOrderStatusUseCase.ExecuteAsync(orderId);
            return NoContent();
        }
        
        [Authorize(Roles = "Restaurant")]
        [HttpGet("history/{restaurantId}")]
        public async Task<IActionResult> GetHistory([FromRoute] Guid restaurantId,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 20)
        {
            var result = await _getOrderHistoryUseCase.ExecuteAsync(restaurantId, page, pageSize);
            return Ok(result);
        }
        
        [Authorize(Roles = "Restaurant")]
        [HttpPut("mark-as-delivered/{orderId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> MarkAsDelivered([FromRoute] Guid orderId)
        {
            await _markAsDeliveredUseCase.ExecuteAsync(orderId);
            return NoContent();
        }
        
        [Authorize(Roles = "Restaurant")]
        [HttpPatch("abandon/{orderId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AbandonOrder([FromRoute] Guid orderId)
        {
            await _abandonOrderUseCase.ExecuteAsync(orderId);
            return NoContent();
        }
        
        [Authorize(Roles = "Restaurant")]
        [HttpPut("{orderId}/cancel/approve/")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> ApproveCancellationRequest([FromRoute] Guid orderId, ApproveCancellationRequestJson request)
        {
            await _approveCancellationRequestUseCase.ExecuteAsync(orderId, request);
            return NoContent();
        }
        
        [Authorize(Roles = "Restaurant")]
        [HttpPut("{orderId}/cancel/reject/")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> RejectCancellationRequest([FromRoute] Guid orderId, RejectCancellationRequestJson request)
        {
            await _rejectCancellationRequestUseCase.ExecuteAsync(orderId, request);
            return NoContent();
        }
        
        [Authorize(Roles = "Restaurant")]
        [HttpPut("{orderId}/reject-order/")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> RejectOrder([FromRoute] Guid orderId, RejectOrderRequestJson request)
        {
            await _rejectOrderUseCase.ExecuteAsync(orderId, request);
            return NoContent();
        }
        
        [Authorize(Roles = "Restaurant")]
        [HttpPut("cancel-order-by-restaurant/{orderId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CancelOrderByRestaurant([FromRoute] Guid orderId, [FromBody] CancelOrderByRestaurantRequestJson request)
        {
            
            await _cancelOrderUseCase.ExecuteAsync(orderId, request);
            return NoContent();
        }
    }
}
