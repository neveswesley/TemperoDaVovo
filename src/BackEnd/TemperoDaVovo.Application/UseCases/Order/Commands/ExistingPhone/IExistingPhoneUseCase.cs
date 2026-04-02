namespace TemperoDaVovo.Application.UseCases.Order.Commands.ExistingPhone;

public interface IExistingPhoneUseCase
{
    Task<string?> ExecuteAsync(string phone);

}