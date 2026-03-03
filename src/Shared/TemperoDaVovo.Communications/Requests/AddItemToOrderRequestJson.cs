namespace TemperoDaVovo.Communications.Requests;

public class AddItemToOrderRequestJson
{
    public Guid RestaurantId { get; set; }
    public string ClientSessionId { get; set; } = string.Empty;
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public string? Observation { get; set; }
    public decimal DeliveryFee { get; set; }
    public List<AddSideDishRequestJson> SideDishes { get; set; } = new List<AddSideDishRequestJson>();
}