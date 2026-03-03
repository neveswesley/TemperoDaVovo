namespace TemperoDaVovo.Communications.Requests;

public class CreateNeighborhoodRequestJson
{
    
    public Guid CityId { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal DeliveryFee { get; set; }
}