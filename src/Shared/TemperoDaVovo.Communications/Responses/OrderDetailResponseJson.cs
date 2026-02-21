namespace TemperoDaVovo.Communications.Responses;

public class OrderDetailResponseJson
{
    public Guid Id { get; set; }
    public Guid RestaurantId { get; set; }
    public string ClientSessionId { get; set; } = string.Empty;
    public decimal Total { get; set; }
    public List<OrderItemResponseJson> Items { get; set; } = new();
}