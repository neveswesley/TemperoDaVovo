namespace TemperoDaVovo.Communications.Requests;

public class VerifyTwoFactorRequestJson
{
    public string Email { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
}