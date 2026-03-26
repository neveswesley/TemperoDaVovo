using TemperoDaVovo.Domain.Enums;

namespace TemperoDaVovo.Communications.Requests;

public class RejectOrderRequestJson
{
    public CancellationReasonType CancellationReasonType { get; set; }
    public string CancellationDescription { get; set; } = string.Empty;
}