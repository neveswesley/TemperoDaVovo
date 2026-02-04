namespace TemperoDaVovo.Communications.Responses;

public class ToggleProductActiveResponseJson
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public string Message { get; set; } = string.Empty;
}