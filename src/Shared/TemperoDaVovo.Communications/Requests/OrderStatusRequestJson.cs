using TemperoDaVovo.Domain.Enums;

namespace TemperoDaVovo.Communications.Requests;

public class OrderStatusRequestJson
{
    public string Status { get; set; } = OrderStatus.PendingConfirmation.ToString();
}