namespace TemperoDaVovo.Communications.Responses;

public class ToggleProductActiveResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public string Message { get; set; } = string.Empty;
}