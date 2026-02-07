namespace TemperoDaVovo.Communications.Requests;

public class UpdateSideDishRequestJson
{
    public string Name { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}