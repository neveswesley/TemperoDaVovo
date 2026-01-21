using TemperoDaVovo.Communications.Requests;
using TemperoDaVovo.Communications.Responses;

namespace TemperoDaVovo.Application.UseCases.User.Login;

public interface ILoginUseCase
{
    Task<LoginUserResponseJson> Execute(LoginUserRequestJson request);
}