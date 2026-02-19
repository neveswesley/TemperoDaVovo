using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TemperoDaVovo.Application.UseCases.Order.Commands.AddItemToOrder;
using TemperoDaVovo.Communications.Requests;

namespace TemperoDaVovo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        
        private readonly IAddItemToOrderUseCase _addItemToOrderUseCase;

        public OrdersController(IAddItemToOrderUseCase addItemToOrderUseCase)
        {
            _addItemToOrderUseCase = addItemToOrderUseCase;
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
        
    }
}
