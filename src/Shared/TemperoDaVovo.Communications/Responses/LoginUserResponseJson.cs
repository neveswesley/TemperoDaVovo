namespace TemperoDaVovo.Communications.Responses;

public class LoginUserResponseJson
{
    public Guid UserId { get; set; }
    public Guid RestaurantId { get; set; }
    public string Token { get; set; } = string.Empty;
    public bool Success { get; set; }
}