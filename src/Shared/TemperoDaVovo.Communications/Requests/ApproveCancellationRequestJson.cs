using TemperoDaVovo.Domain.Enums;

namespace TemperoDaVovo.Communications.Requests;

public class ApproveCancellationRequestJson
{
    public CancellationReasonType CancellationReasonType { get; set; }
    public string CancellationReason { get; set; } = string.Empty;
}