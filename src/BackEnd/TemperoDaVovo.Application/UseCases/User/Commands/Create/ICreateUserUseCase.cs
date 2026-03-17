using TemperoDaVovo.Communications.Requests;
using TemperoDaVovo.Communications.Responses;

namespace TemperoDaVovo.Application.UseCases.User.Create;

public interface ICreateUserUseCase
{
    Task<CreateUserResponseJson> Execute(CreateUserRequestJson request);
}