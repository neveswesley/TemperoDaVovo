namespace TemperoDaVovo.Communications.Requests;

public class CreateCityRequestJson
{
    public string Name { get; set; } = string.Empty;
    public Guid RestaurantId { get; set; }
}