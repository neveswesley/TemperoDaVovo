using Microsoft.AspNetCore.Mvc;
using TemperoDaVovo.Application.UseCases.Restaurant;
using TemperoDaVovo.Application.UseCases.Restaurant.Create;
using TemperoDaVovo.Communications.Requests;
using TemperoDaVovo.Communications.Responses;

namespace TemperoDaVovo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        
        private readonly ICreateRestaurantUseCase _createRestaurantUseCase;

        public RestaurantController(ICreateRestaurantUseCase createRestaurantUseCase)
        {
            _createRestaurantUseCase = createRestaurantUseCase;
        }

        [HttpPost]
        public async Task<CreateRestaurantResponseJson> Post([FromBody] CreateRestaurantRequestJson request)
        {
            var result = await _createRestaurantUseCase.Execute(request);
            return result;
        }
    }
}