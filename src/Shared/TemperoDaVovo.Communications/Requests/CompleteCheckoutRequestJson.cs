namespace TemperoDaVovo.Communications.Requests;

public class CompleteCheckoutRequestJson
{
    public Guid OrderId { get; set; }
    public string Phone { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
}