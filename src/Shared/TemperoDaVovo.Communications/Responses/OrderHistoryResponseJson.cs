using TemperoDaVovo.Domain.Enums;

namespace TemperoDaVovo.Communications.Responses;

public class OrderHistoryResponseJson
{
    public int OrderNumber { get; set; }
    public DateTime CreatedAt { get; set; }
    public decimal Total { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public string CustomerPhone { get; set; } = string.Empty;
    public OrderStatus Status { get; set; }
    public PaymentResponseJson? Payment { get; set; }
    public DeliveryMode DeliveryMode { get; set; }
    public ICollection<OrderItemResponseJson> Items { get; set; } = new List<OrderItemResponseJson>();
}