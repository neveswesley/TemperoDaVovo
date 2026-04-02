using TemperoDaVovo.Communications.Responses;

namespace TemperoDaVovo.Application.UseCases.User.Commands.VerifyTwoFactor;

public interface IVerifyTwoFactorUseCase
{
    Task<LoginUserResponseJson> ExecuteAsync(string email, string code);
}