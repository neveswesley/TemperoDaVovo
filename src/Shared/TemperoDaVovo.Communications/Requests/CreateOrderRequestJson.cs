namespace TemperoDaVovo.Communications.Requests;

public class CreateOrderRequestJson
{
    public Guid RestaurantId { get; set; }
    public string ClientSessionId { get; set; } = string.Empty;
    public string CustomerName { get; set; } = string.Empty;
    public string CustomerPhone { get; set; } = string.Empty;
}