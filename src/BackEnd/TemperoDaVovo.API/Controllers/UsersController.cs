using Microsoft.AspNetCore.Mvc;
using TemperoDaVovo.Application.UseCases.User.Create;
using TemperoDaVovo.Application.UseCases.User.Login;
using TemperoDaVovo.Communications.Requests;
using TemperoDaVovo.Communications.Responses;

namespace TemperoDaVovo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        
        private readonly ICreateUserUseCase _createUserUseCase;
        private readonly ILoginUseCase _loginUseCase;

        public UsersController(ICreateUserUseCase createUserUseCase, ILoginUseCase loginUseCase)
        {
            _createUserUseCase = createUserUseCase;
            _loginUseCase = loginUseCase;
        }

        [HttpPost]
        [ProducesResponseType(typeof(CreateUserResponseJson), StatusCodes.Status201Created)]
        public async Task<IActionResult> Post([FromBody] CreateUserRequestJson createUserRequestJson)
        {
            var register = await _createUserUseCase.Execute(createUserRequestJson);
            return Created(string.Empty, register);
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(LoginUserResponseJson), StatusCodes.Status200OK)]
        public async Task<IActionResult> Post([FromBody] LoginUserRequestJson loginUserRequestJson)
        {
            var login = await _loginUseCase.Execute(loginUserRequestJson);
            return Ok(login);
        }
    }
}
