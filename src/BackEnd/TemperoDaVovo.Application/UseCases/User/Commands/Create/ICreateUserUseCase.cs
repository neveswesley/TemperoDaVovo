using TemperoDaVovo.Communications.Requests;
using TemperoDaVovo.Communications.Responses;

namespace TemperoDaVovo.Application.UseCases.User.Commands.Create;

public interface ICreateUserUseCase
{
    Task<CreateUserResponseJson> ExecuteAsync(CreateUserRequestJson request);
}