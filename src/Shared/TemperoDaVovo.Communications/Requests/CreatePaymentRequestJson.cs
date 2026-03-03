using TemperoDaVovo.Domain.Enums;

namespace TemperoDaVovo.Communications.Requests;

public class CreatePaymentRequestJson
{
    public Guid OrderId { get; set; }
    public PaymentWay PaymentWay { get; set; }
    public PaymentStatus PaymentStatus { get; set; }
    public decimal AmountPaid { get; set; }
    public decimal ToReceive { get; set; }
    public decimal Change { get; set; }
    public string? TransactionId { get; set; }
}