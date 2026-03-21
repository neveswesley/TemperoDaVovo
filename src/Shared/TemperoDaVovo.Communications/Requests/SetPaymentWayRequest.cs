using TemperoDaVovo.Domain.Enums;

namespace TemperoDaVovo.Communications.Requests;

public class SetPaymentWayRequest
{
    public List<PaymentWay> PaymentWays { get; set; } = [];
}