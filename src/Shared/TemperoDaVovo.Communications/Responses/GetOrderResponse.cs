using TemperoDaVovo.Domain.Entities;
using TemperoDaVovo.Domain.Enums;

namespace TemperoDaVovo.Communications.Responses;

public class GetOrderResponse
{
    public Guid Id { get; set; }
    public int OrderNumber { get; set; }
    public string CustomerName { get; set; } =  string.Empty;
    public string CustomerPhone { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public OrderStatus Status { get; set; }
    public decimal SubTotal { get; set; }
    public decimal? DeliveryFee { get; set; }
    public decimal Total { get; set; }
    public DeliveryMode DeliveryMode { get; set; }
    public string? Street { get; set; }
    public string? Number { get; set; }
    public string? Neighborhood { get; set; }
    public string? City { get; set; }
    public string? Reference { get; set; }
    public string? Complement { get; set; }
    public int EstimatedDeliveryTimeInMinutes { get; set; }
    public PaymentResponseJson? Payment { get; set; }
    public DateTime? PreparingStartedAt { get; set; }
    public DateTime? PendingConfirmationAt { get; set; }
    public DateTime? OnTheWayAt { get; set; }
    public DateTime? ReadyAt { get; set; }
    public DateTime? CanceledAt { get; set; }
    public List<OrderItemResponseJson> Items { get; set; } = new();
}