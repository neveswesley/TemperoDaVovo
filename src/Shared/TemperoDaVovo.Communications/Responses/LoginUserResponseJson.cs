namespace TemperoDaVovo.Communications.Responses;

public class LoginUserResponseJson
{
    public bool Success { get; set; }
    public Guid? UserId { get; set; }
    public Guid? RestaurantId { get; set; }
    public string? Message { get; set; }
}