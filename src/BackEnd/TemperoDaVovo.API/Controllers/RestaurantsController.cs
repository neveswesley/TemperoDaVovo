using Microsoft.AspNetCore.Mvc;
using TemperoDaVovo.Application.UseCases.Restaurant.Create;
using TemperoDaVovo.Communications.Requests;
using TemperoDaVovo.Communications.Responses;

namespace TemperoDaVovo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantsController : ControllerBase
    {
        
        private readonly ICreateRestaurantUseCase _createRestaurantUseCase;

        public RestaurantsController(ICreateRestaurantUseCase createRestaurantUseCase)
        {
            _createRestaurantUseCase = createRestaurantUseCase;
        }

        [HttpPost]
        [ProducesResponseType(typeof(CreateRestaurantResponseJson), StatusCodes.Status201Created)]
        public async Task<IActionResult> Post([FromBody] CreateRestaurantRequestJson request)
        {
            var result = await _createRestaurantUseCase.Execute(request);
            return Created(string.Empty, result);
        }
    }
}