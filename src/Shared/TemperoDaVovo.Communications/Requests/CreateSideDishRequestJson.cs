namespace TemperoDaVovo.Communications.Requests;

public class CreateSideDishRequestJson
{
    public Guid SideDishGroupId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}