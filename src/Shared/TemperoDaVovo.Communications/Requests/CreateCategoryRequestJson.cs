namespace TemperoDaVovo.Communications.Requests;

public class CreateCategoryRequestJson
{
    public Guid RestaurantId { get; set; }
    public string Name { get; set; } = string.Empty;
}