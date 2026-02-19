namespace TemperoDaVovo.Communications.Responses;

public class SideDishResponseJson
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}