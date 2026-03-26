using TemperoDaVovo.Domain.Enums;

namespace TemperoDaVovo.Communications.Requests;

public class CancelOrderByRestaurantRequestJson
{
    public CancellationReasonType Reason { get; set; }
    public string CancellationDescription { get; set; } = string.Empty;
    public CanceledBy CanceledBy { get; set; } = CanceledBy.Restaurant;
}