namespace TemperoDaVovo.Communications.Responses;

public class OrderDetailResponseJson
{
    public Guid Id { get; set; }
    public Guid RestaurantId { get; set; }
    public string ClientSessionId { get; set; } = string.Empty;
    public int OrderNumber { get; set; }
    public decimal SubTotal { get; set; }
    public decimal Total { get; set; }
    public decimal? DeliveryFee { get; set; }
    public string Status { get; set; } = string.Empty;
    public string DeliveryMode { get; set; } = string.Empty;
    public string? Street { get; set; }
    public string? Number { get; set; }
    public string? Complement { get; set; }
    public string? Neighborhood { get; set; }
    public string? City { get; set; }
    public string? PaymentWay { get; set; }
    public int EstimatedDeliveryTimeInMinutes { get; set; }
    public DateTime CreatedAt { get; set; }

    public DateTime? PreparingStartedAt { get; set; }
    public DateTime? OnTheWayAt { get; set; }
    public DateTime? ReadyAt { get; set; }
    public DateTime? CanceledAt { get; set; }

    public List<OrderItemResponseJson> Items { get; set; } = new();
}