using TemperoDaVovo.Communications.Responses;

namespace TemperoDaVovo.Application.UseCases.User.Queries.Get;

public interface IGetUserUseCase
{
    Task<GetUserResponse> ExecuteAsync(Guid userId);
}