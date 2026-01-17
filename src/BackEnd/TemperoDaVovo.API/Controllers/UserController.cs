using Microsoft.AspNetCore.Mvc;
using TemperoDaVovo.Application.UseCases.User.Create;
using TemperoDaVovo.Communications.Requests;

namespace TemperoDaVovo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        
        private readonly ICreateUserUseCase _createUserUseCase;

        public UserController(ICreateUserUseCase createUserUseCase)
        {
            _createUserUseCase = createUserUseCase;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateUserRequestJson createUserRequestJson)
        {
            var register = await _createUserUseCase.Execute(createUserRequestJson);
            return Created(string.Empty, register);

        }
    }
}
