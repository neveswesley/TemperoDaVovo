namespace TemperoDaVovo.Communications.Responses;

public class RestaurantResponseJson
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsOpenNow { get; set; }
    public List<OpeningHourResponse> OpeningHours { get; set; } = [];
}