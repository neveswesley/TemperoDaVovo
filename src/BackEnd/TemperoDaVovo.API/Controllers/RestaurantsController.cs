using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TemperoDaVovo.Application.UseCases.Restaurant.Commands.Create;
using TemperoDaVovo.Application.UseCases.Restaurant.Commands.Update;
using TemperoDaVovo.Application.UseCases.Restaurant.Commands.UpdatePaymentWay;
using TemperoDaVovo.Application.UseCases.Restaurant.Create;
using TemperoDaVovo.Application.UseCases.Restaurant.Queries.Get;
using TemperoDaVovo.Application.UseCases.RestaurantOpeningHour.Get;
using TemperoDaVovo.Application.UseCases.RestaurantOpeningHour.Update;
using TemperoDaVovo.Communications.Requests;
using TemperoDaVovo.Communications.Responses;

namespace TemperoDaVovo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantsController : ControllerBase
    {
        private readonly ICreateRestaurantUseCase _createRestaurantUseCase;
        private readonly IOpeningHoursUseCase _openingHoursUseCase;
        private readonly IGetOpeningHoursUseCase _getOpeningHoursUseCase;
        private readonly IGetRestaurantByIdUseCase _getRestaurantByIdUseCase;
        private readonly IUpdateRestaurantUseCase _updateRestaurantUseCase;
        private readonly IUpdatePaymentWayUseCase _updatePaymentWayUseCase;

        public RestaurantsController(ICreateRestaurantUseCase createRestaurantUseCase, IOpeningHoursUseCase openingHoursUseCase, IGetOpeningHoursUseCase getOpeningHoursUseCase, IGetRestaurantByIdUseCase getRestaurantByIdUseCase, IUpdateRestaurantUseCase updateRestaurantUseCase, IUpdatePaymentWayUseCase updatePaymentWayUseCase)
        {
            _createRestaurantUseCase = createRestaurantUseCase;
            _openingHoursUseCase = openingHoursUseCase;
            _getOpeningHoursUseCase = getOpeningHoursUseCase;
            _getRestaurantByIdUseCase = getRestaurantByIdUseCase;
            _updateRestaurantUseCase = updateRestaurantUseCase;
            _updatePaymentWayUseCase = updatePaymentWayUseCase;
        }

        [HttpPost]
        [ProducesResponseType(typeof(RestaurantResponseJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(RestaurantResponseJson), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(RestaurantResponseJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] CreateRestaurantRequestJson request)
        {
            var result = await _createRestaurantUseCase.ExecuteAsync(request);
            return Created(string.Empty, result);
        }

        [Authorize(Roles = "Restaurant")]
        [HttpPut("opening-hours/{restaurantId}")]
        [ProducesResponseType(typeof(RestaurantResponseJson), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(RestaurantResponseJson), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(RestaurantResponseJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateOpeningHours([FromRoute] Guid restaurantId, [FromBody] UpdateRestaurantOpeningHoursRequest request)
        {
            await _openingHoursUseCase.ExecuteAsync(restaurantId, request);
            return NoContent();
        }
        
        [HttpGet("opening-hours/{restaurantId}")]
        [ProducesResponseType(typeof(RestaurantResponseJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(RestaurantResponseJson), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(RestaurantResponseJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get([FromRoute] Guid restaurantId)
        {
            var result = await _getOpeningHoursUseCase.ExecuteAsync(restaurantId);
            return Ok(result);
        }
        
        [HttpGet("{restaurantId}")]
        [ProducesResponseType(typeof(RestaurantResponseJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(RestaurantResponseJson), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(RestaurantResponseJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetRestaurants([FromRoute] Guid restaurantId)
        {
            var result = await _getRestaurantByIdUseCase.ExecuteAsync(restaurantId);
            return Ok(result);
        }
        
        [Authorize(Roles = "Restaurant")]
        [HttpPut("{restaurantId}")]
        [ProducesResponseType(typeof(RestaurantResponseJson), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(RestaurantResponseJson), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(RestaurantResponseJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromRoute] Guid restaurantId, UpdateRestaurantRequest request)
        {
            await _updateRestaurantUseCase.ExecuteAsync(restaurantId, request);
            return NoContent();
        }
        
        [Authorize(Roles = "Restaurant")]
        [HttpPut("update-payment-way/{restaurantId}")]
        [ProducesResponseType(typeof(RestaurantResponseJson), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(RestaurantResponseJson), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(RestaurantResponseJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePaymentWay([FromRoute] Guid restaurantId, [FromBody] SetPaymentWayRequest request)
        {
            await _updatePaymentWayUseCase.ExecuteAsync(restaurantId, request);
            return NoContent();
        }
    }
}