namespace TemperoDaVovo.Application.UseCases.Order.Commands.ExistingPhone;

public interface IExistingPhoneUseCase
{
    Task<string?> Execute(string phone);

}