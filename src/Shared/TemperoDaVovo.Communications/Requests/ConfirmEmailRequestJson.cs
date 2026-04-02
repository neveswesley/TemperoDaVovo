namespace TemperoDaVovo.Communications.Requests;

public class ConfirmEmailRequestJson
{
    public string Email { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
}