namespace TemperoDaVovo.Communications.Requests;

public class UpdateNeighborhoodRequestJson
{
    public string Name { get; set; } = string.Empty;
    public decimal DeliveryFee { get; set; }
    public int BaseDeliveryTimeInMinutes { get; set; }
}