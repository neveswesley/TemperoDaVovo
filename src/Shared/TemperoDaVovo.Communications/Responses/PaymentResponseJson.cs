using TemperoDaVovo.Domain.Enums;

namespace TemperoDaVovo.Communications.Responses;

public class PaymentResponseJson
{
    public PaymentWay PaymentWay { get; set; }
    public PaymentStatus Status { get; set; }
    public decimal Total { get; set; }
    public decimal AmountPaid { get; set; }
    public decimal Change { get; set; }
}