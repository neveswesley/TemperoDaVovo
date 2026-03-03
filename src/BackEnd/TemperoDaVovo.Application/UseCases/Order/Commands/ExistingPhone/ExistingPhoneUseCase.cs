using TemperoDaVovo.Domain.Interfaces.ReadOnly;

namespace TemperoDaVovo.Application.UseCases.Order.Commands.ExistingPhone;

public class ExistingPhoneUseCase : IExistingPhoneUseCase
{
    private readonly IOrderReadOnlyRepository _orderReadOnlyRepository;

    public ExistingPhoneUseCase(IOrderReadOnlyRepository orderReadOnlyRepository)
    {
        _orderReadOnlyRepository = orderReadOnlyRepository;
    }

    public async Task<string?> Execute(string phone)
    {
        return await _orderReadOnlyRepository.ExistingPhone(phone);
    }
}