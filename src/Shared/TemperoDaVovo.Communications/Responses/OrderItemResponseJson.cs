namespace TemperoDaVovo.Communications.Responses;

public class OrderItemResponseJson
{
    public Guid Id { get; set; }
    public Guid? ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public string? Observation { get; set; }
    public decimal Total { get; set; }
    public List<OrderSideDishResponseJson> SideDishes { get; set; } = new();
}