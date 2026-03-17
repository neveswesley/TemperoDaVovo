using TemperoDaVovo.Communications.Responses;

namespace TemperoDaVovo.Application.UseCases.User.Get;

public interface IGetUserUseCase
{
    Task<GetUserResponse> ExecuteAsync(Guid userId);
}