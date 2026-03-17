using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TemperoDaVovo.Application.UseCases.User.Commands.UpdatePassword;
using TemperoDaVovo.Application.UseCases.User.Create;
using TemperoDaVovo.Application.UseCases.User.Get;
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
        private readonly IGetUserUseCase _getUserUseCase;
        private readonly IUpdatePasswordUseCase _updatePasswordUseCase;

        public UsersController(ICreateUserUseCase createUserUseCase, ILoginUseCase loginUseCase, IGetUserUseCase getUserUseCase, IUpdatePasswordUseCase updatePasswordUseCase)
        {
            _createUserUseCase = createUserUseCase;
            _loginUseCase = loginUseCase;
            _getUserUseCase = getUserUseCase;
            _updatePasswordUseCase = updatePasswordUseCase;
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
        
        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> GetMe()
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var result = await _getUserUseCase.ExecuteAsync(userId);
            return Ok(result);
        }

        [HttpPut("update-password/{userId}")]
        public async Task<IActionResult> UpdatePassword(Guid userId, UpdatePasswordRequest request)
        {
            await _updatePasswordUseCase.Execute(userId, request);
            return NoContent();
        }
    }
}
