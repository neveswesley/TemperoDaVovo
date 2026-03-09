using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TemperoDaVovo.Application.UseCases.City.Commands.Create;
using TemperoDaVovo.Application.UseCases.City.Commands.Update;
using TemperoDaVovo.Application.UseCases.City.Queries.GetAll;
using TemperoDaVovo.Application.UseCases.City.Queries.GetById;
using TemperoDaVovo.Communications.Requests;

namespace TemperoDaVovo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        
        private readonly ICreateCityUseCase _createCityUseCase;
        private readonly IGetCityByIdUseCase _getCityByIdUseCase;
        private readonly IGetAllCitiesByRestaurantId _getAllCitiesByIdUseCase;
        private readonly IUpdateCityUseCase _updateCityUseCase;

        public CitiesController(ICreateCityUseCase createCityUseCase, IGetCityByIdUseCase getCityByIdUseCase, IGetAllCitiesByRestaurantId getAllCitiesByIdUseCase, IUpdateCityUseCase updateCityUseCase)
        {
            _createCityUseCase = createCityUseCase;
            _getCityByIdUseCase = getCityByIdUseCase;
            _getAllCitiesByIdUseCase = getAllCitiesByIdUseCase;
            _updateCityUseCase = updateCityUseCase;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Create(CreateCityRequestJson request)
        {
            var result = await _createCityUseCase.ExecuteAsync(request);
            return Created(string.Empty, result);
        }

        [HttpGet("{cityId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([FromRoute] Guid cityId)
        {
            var result = await _getCityByIdUseCase.ExecuteAsync(cityId);
            return Ok(result);
        }
        
        [HttpGet("{restaurantId}/restaurants")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAll([FromRoute] Guid restaurantId)
        {
            var result = await _getAllCitiesByIdUseCase.ExecuteAsync(restaurantId);
            return Ok(result);
        }
        
        [HttpPut("update-city/{cityId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromBody] UpdateCityRequestJson request, [FromRoute] Guid cityId)
        {
            var result = await _updateCityUseCase.ExecuteAsync(cityId, request);
            return Ok(result);
        }
        
    }
}
