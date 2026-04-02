namespace TemperoDaVovo.Application.UseCases.User.Commands.ConfirmEmail;

public interface IConfirmEmailUseCase
{
    Task ExecuteAsync(string email, string code);
}