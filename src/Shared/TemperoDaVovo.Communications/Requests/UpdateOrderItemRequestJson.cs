using TemperoDaVovo.Communications.Responses;

namespace TemperoDaVovo.Communications.Requests;

public class UpdateOrderItemRequest
{
    public int Quantity { get; set; }
    public string? Observation { get; set; }
    public List<UpdateOrderItemSideDishJson> SideDishes { get; set; } = new();
}