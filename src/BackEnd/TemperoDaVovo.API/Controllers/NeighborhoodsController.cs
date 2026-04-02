using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TemperoDaVovo.Application.UseCases.Neighborhood.Commands.Create;
using TemperoDaVovo.Application.UseCases.Neighborhood.Commands.Delete;
using TemperoDaVovo.Application.UseCases.Neighborhood.Commands.Update;
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
        private readonly IDeleteNeighborhoodUseCase _deleteNeighborhoodUseCase;
        private readonly IUpdateNeighborhoodUseCase _updateNeighborhoodUseCase;

        public NeighborhoodsController(ICreateNeighborhoodUseCase createNeighborhoodUseCase, IGetAllNeighborhoodByRestaurantId getAllNeighborhoodByRestaurantId, IDeleteNeighborhoodUseCase deleteNeighborhoodUseCase, IUpdateNeighborhoodUseCase updateNeighborhoodUseCase)
        {
            _createNeighborhoodUseCase = createNeighborhoodUseCase;
            _getAllNeighborhoodByRestaurantId = getAllNeighborhoodByRestaurantId;
            _deleteNeighborhoodUseCase = deleteNeighborhoodUseCase;
            _updateNeighborhoodUseCase = updateNeighborhoodUseCase;
        }

        [Authorize(Roles = "Restaurant")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(CreateNeighborhoodRequestJson neighborhoodRequest)
        {
            var result = await _createNeighborhoodUseCase.ExecuteAsync(neighborhoodRequest);
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
        
        [Authorize(Roles = "Restaurant")]
        [HttpPut("{neighborhoodId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromRoute] Guid neighborhoodId, UpdateNeighborhoodRequestJson request)
        {
            await _updateNeighborhoodUseCase.ExecuteAsync(neighborhoodId, request);
            return NoContent();
        }
        
        [Authorize(Roles = "Restaurant")]
        [HttpPut("delete/{neighborhoodId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Deactivate([FromRoute] Guid neighborhoodId)
        {
            await _deleteNeighborhoodUseCase.ExecuteAsync(neighborhoodId);
            return NoContent();
        }
    }
}
