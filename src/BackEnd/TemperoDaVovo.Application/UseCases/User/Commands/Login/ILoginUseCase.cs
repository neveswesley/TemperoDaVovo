using TemperoDaVovo.Communications.Requests;
using TemperoDaVovo.Communications.Responses;

namespace TemperoDaVovo.Application.UseCases.User.Commands.Login;

public interface ILoginUseCase
{
    Task<LoginUserResponseJson> ExecuteAsync(LoginUserRequestJson request);
}