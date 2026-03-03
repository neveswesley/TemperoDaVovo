using TemperoDaVovo.Domain.Enums;

namespace TemperoDaVovo.Communications.Responses;

public class GetOrderByClientResponse
{
    public Guid Id { get; set; }
    public int OrderNumber { get; set; }
    public DateTime CreatedAt { get; set; }
    public OrderStatus Status { get; set; }
    public decimal SubTotal { get; set; }
    public decimal? DeliveryFee { get; set; }
    public decimal Total { get; set; }
    public DeliveryMode DeliveryMode { get; set; }
    public string? Street { get; set; }
    public string? Number { get; set; }
    public string? Complement { get; set; }
    public PaymentWay? PaymentWay { get; set; }
    public List<OrderItemResponseJson> Items { get; set; } = new();
}