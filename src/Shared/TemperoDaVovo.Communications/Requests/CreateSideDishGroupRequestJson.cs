namespace TemperoDaVovo.Communications.Requests;

public class CreateSideDishGroupRequestJson
{
    public Guid RestaurantId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int MinQuantity { get; set; }
    public int MaxQuantity { get; set; }
}