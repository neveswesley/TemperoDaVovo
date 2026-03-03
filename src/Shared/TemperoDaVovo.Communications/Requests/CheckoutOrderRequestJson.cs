using TemperoDaVovo.Domain.Enums;

namespace TemperoDaVovo.Communications.Requests;

public class CheckoutOrderRequestJson
{
    public Guid OrderId { get; set; }

    // Endereço
    public DeliveryMode DeliveryMode { get; set; }
    public string? Street { get; set; } = string.Empty;
    public string? Number { get; set; } = string.Empty;
    public string? Complement { get; set; }
    public string? Reference { get; set; }
    public Guid? NeighborhoodId { get; set; }
    public string? City { get; set; } = string.Empty;
    public AddressName? AddressName { get; set; }

    // Pagamento
    public PaymentWay PaymentWay { get; set; }
    public decimal? AmountGiven { get; set; } // só se dinheiro
    public decimal DeliveryFee { get; set; }
}