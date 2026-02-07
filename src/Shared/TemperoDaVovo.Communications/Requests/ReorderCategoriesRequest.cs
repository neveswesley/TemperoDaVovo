namespace TemperoDaVovo.Communications.Requests;

public class ReorderCategoriesRequest
{
    public Guid RestaurantId { get; set; }
    public List<Guid> CategoryIds { get; set; }
}