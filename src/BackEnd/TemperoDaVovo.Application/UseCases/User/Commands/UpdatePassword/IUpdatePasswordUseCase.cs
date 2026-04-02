using TemperoDaVovo.Communications.Requests;

namespace TemperoDaVovo.Application.UseCases.User.Commands.UpdatePassword;

public interface IUpdatePasswordUseCase
{
    Task ExecuteAsync(Guid userId, UpdatePasswordRequest request);
}