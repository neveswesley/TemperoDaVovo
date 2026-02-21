namespace TemperoDaVovo.Communications.Responses;

public class OrderSideDishResponseJson
{
    public Guid SideDishId { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
}