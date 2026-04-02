using TemperoDaVovo.Communications.Requests;
using TemperoDaVovo.Communications.Responses;

namespace TemperoDaVovo.Application.UseCases.Order.Commands.AddItemToOrder;

public interface IAddItemToOrderUseCase
{
    Task<OrderResponseJson> ExecuteAsync(AddItemToOrderRequestJson request);
}