using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TemperoDaVovo.Application.UseCases.User.Commands.ConfirmEmail;
using TemperoDaVovo.Application.UseCases.User.Commands.Create;
using TemperoDaVovo.Application.UseCases.User.Commands.Login;
using TemperoDaVovo.Application.UseCases.User.Commands.UpdatePassword;
using TemperoDaVovo.Application.UseCases.User.Commands.VerifyTwoFactor;
using TemperoDaVovo.Application.UseCases.User.Create;
using TemperoDaVovo.Application.UseCases.User.Login;
using TemperoDaVovo.Application.UseCases.User.Queries.Get;
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
        private readonly IConfirmEmailUseCase _confirmEmailUseCase;
        private readonly IVerifyTwoFactorUseCase _verifyTwoFactorUseCase;

        public UsersController(ICreateUserUseCase createUserUseCase, ILoginUseCase loginUseCase, IGetUserUseCase getUserUseCase, IUpdatePasswordUseCase updatePasswordUseCase, IConfirmEmailUseCase confirmEmailUseCase, IVerifyTwoFactorUseCase verifyTwoFactorUseCase)
        {
            _createUserUseCase = createUserUseCase;
            _loginUseCase = loginUseCase;
            _getUserUseCase = getUserUseCase;
            _updatePasswordUseCase = updatePasswordUseCase;
            _confirmEmailUseCase = confirmEmailUseCase;
            _verifyTwoFactorUseCase = verifyTwoFactorUseCase;
        }

        [HttpPost]
        [ProducesResponseType(typeof(CreateUserResponseJson), StatusCodes.Status201Created)]
        public async Task<IActionResult> Post([FromBody] CreateUserRequestJson createUserRequestJson)
        {
            var register = await _createUserUseCase.ExecuteAsync(createUserRequestJson);
            return Created(string.Empty, register);
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(LoginUserResponseJson), StatusCodes.Status200OK)]
        public async Task<IActionResult> Post([FromBody] LoginUserRequestJson loginUserRequestJson)
        {
            var login = await _loginUseCase.ExecuteAsync(loginUserRequestJson);
            return Ok(login);
        }
        
        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> GetMe()
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var result = await _getUserUseCase.ExecuteAsync(userId);
            return Ok(result);
        }

        [Authorize]
        [HttpPut("update-password/{userId}")]
        public async Task<IActionResult> UpdatePassword(Guid userId, UpdatePasswordRequest request)
        {
            await _updatePasswordUseCase.ExecuteAsync(userId, request);
            return NoContent();
        }
        
        [HttpPost("confirm-email")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailRequestJson request)
        {
            await _confirmEmailUseCase.ExecuteAsync(request.Email, request.Code);
            return NoContent();
        }

        [HttpPost("verify-2fa")]
        [ProducesResponseType(typeof(LoginUserResponseJson), StatusCodes.Status200OK)]
        public async Task<IActionResult> VerifyTwoFactor([FromBody] VerifyTwoFactorRequestJson request)
        {
            var result = await _verifyTwoFactorUseCase.ExecuteAsync(request.Email, request.Code);
            return Ok(result);
        }
        
    }
}
