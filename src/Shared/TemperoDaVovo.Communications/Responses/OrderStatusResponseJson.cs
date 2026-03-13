using TemperoDaVovo.Domain.Enums;

namespace TemperoDaVovo.Communications.Responses;

public class OrderStatusResponseJson
{
    public OrderStatus Value { get; set; }
    public string Label { get; set; } = string.Empty;
}