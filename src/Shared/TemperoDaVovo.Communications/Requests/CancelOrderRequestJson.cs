using TemperoDaVovo.Domain.Enums;

namespace TemperoDaVovo.Communications.Requests;

public class CancelOrderRequestJson
{
    public Guid OrderId { get; set; }
    public string ClientSessionId { get; set; } = string.Empty;
    public CancellationReasonType Reason { get; set; }
    public CanceledBy CanceledBy { get; set; }
    public string? Description { get; set; }
}