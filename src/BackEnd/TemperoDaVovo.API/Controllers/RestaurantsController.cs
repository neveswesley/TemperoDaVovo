using Microsoft.AspNetCore.Mvc;
using TemperoDaVovo.Application.UseCases.Restaurant.Create;
using TemperoDaVovo.Application.UseCases.Restaurant.Queries.Get;
using TemperoDaVovo.Application.UseCases.RestaurantOpeningHour.Get;
using TemperoDaVovo.Application.UseCases.RestaurantOpeningHour.OpeningHours;
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

        public RestaurantsController(ICreateRestaurantUseCase createRestaurantUseCase, IOpeningHoursUseCase openingHoursUseCase, IGetOpeningHoursUseCase getOpeningHoursUseCase, IGetRestaurantByIdUseCase getRestaurantByIdUseCase)
        {
            _createRestaurantUseCase = createRestaurantUseCase;
            _openingHoursUseCase = openingHoursUseCase;
            _getOpeningHoursUseCase = getOpeningHoursUseCase;
            _getRestaurantByIdUseCase = getRestaurantByIdUseCase;
        }

        [HttpPost]
        [ProducesResponseType(typeof(RestaurantResponseJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(RestaurantResponseJson), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(RestaurantResponseJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] CreateRestaurantRequestJson request)
        {
            var result = await _createRestaurantUseCase.Execute(request);
            return Created(string.Empty, result);
        }

        [HttpPut("opening-hours/{restaurantId}")]
        [ProducesResponseType(typeof(RestaurantResponseJson), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(RestaurantResponseJson), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(RestaurantResponseJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateOpeningHours([FromRoute] Guid restaurantId, [FromBody] UpdateRestaurantOpeningHoursRequest request)
        {
            await _openingHoursUseCase.Execute(restaurantId, request);
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
        
    }
}