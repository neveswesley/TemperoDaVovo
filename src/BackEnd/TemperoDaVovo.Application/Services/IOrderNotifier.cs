namespace TemperoDaVovo.Application.Services;

public interface IOrderNotifier
{
    Task NotifyOrderCreated(Guid restaurantId, object payload);
    Task NotifyOrderUpdated(Guid restaurantId, object payload);
    Task NotifyCustomerOrderUpdated(string clientSessionId, object payload);

}