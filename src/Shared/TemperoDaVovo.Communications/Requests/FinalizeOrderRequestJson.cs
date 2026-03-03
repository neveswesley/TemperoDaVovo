using TemperoDaVovo.Communications.Responses;
using TemperoDaVovo.Domain.Enums;

namespace TemperoDaVovo.Communications.Requests;

public class FinalizeOrderRequestJson
{
    public Guid OrderId { get; set; }
    public string Street { get; set; } = string.Empty;
    public string Number { get; set; } = string.Empty;
    public string Complement { get; set; } = string.Empty;
    public string Reference { get; set; } = string.Empty;
    public Guid NeighborhoodId { get; set; }
    public string City { get; set; } = string.Empty;
    public AddressName AddressName { get; set; }
    public string PaymentStatus { get; set; } = string.Empty;
    public OrderStatus Status { get; set; } = OrderStatus.PendingConfirmation;
    public decimal DeliveryFee { get; set; }
    
}