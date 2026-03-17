using TemperoDaVovo.Communications.Requests;

namespace TemperoDaVovo.Application.UseCases.User.Commands.UpdatePassword;

public interface IUpdatePasswordUseCase
{
    Task Execute(Guid userId, UpdatePasswordRequest request);
}