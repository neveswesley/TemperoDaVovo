using TemperoDaVovo.Communications.Responses;

namespace TemperoDaVovo.Application.UseCases.Order.Queries.GetOrderByCliente;

public interface IGetOrderByClientUseCase
{
    Task<List<GetOrderResponse>> Execute(string clientId);
}