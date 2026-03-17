namespace TemperoDaVovo.Communications.Responses;

public class GetUserResponse
{
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
}