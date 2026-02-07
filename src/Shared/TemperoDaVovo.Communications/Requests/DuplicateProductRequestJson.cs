namespace TemperoDaVovo.Communications.Requests;

public class DuplicateProductRequestJson
{
    public Guid ProductId { get; set; }
    public string NewProductName { get; set; } = string.Empty;
}