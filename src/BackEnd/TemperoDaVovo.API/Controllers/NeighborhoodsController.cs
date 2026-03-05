using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TemperoDaVovo.Application.UseCases.Neighborhood.Commands.Create;
using TemperoDaVovo.Application.UseCases.Neighborhood.Queries.GetAll;
using TemperoDaVovo.Communications.Requests;
using TemperoDaVovo.Domain.Interfaces.WriteOnly;

namespace TemperoDaVovo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NeighborhoodsController : ControllerBase
    {
        
        private readonly ICreateNeighborhoodUseCase _createNeighborhoodUseCase;
        private readonly IGetAllNeighborhoodByRestaurantId _getAllNeighborhoodByRestaurantId;

        public NeighborhoodsController(ICreateNeighborhoodUseCase createNeighborhoodUseCase, IGetAllNeighborhoodByRestaurantId getAllNeighborhoodByRestaurantId)
        {
            _createNeighborhoodUseCase = createNeighborhoodUseCase;
            _getAllNeighborhoodByRestaurantId = getAllNeighborhoodByRestaurantId;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(CreateNeighborhoodRequestJson neighborhoodRequest)
        {
            var result = await _createNeighborhoodUseCase.Execute(neighborhoodRequest);
            return Created(string.Empty, result);
        }
        
        [HttpGet("{restaurantId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAll([FromRoute] Guid restaurantId)
        {
            var result = await _getAllNeighborhoodByRestaurantId.Execute(restaurantId);
            return Ok(result);
        }
    }
}
