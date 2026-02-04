namespace TemperoDaVovo.Communications.Requests;

public class LinkSideDishGroupsToProductRequest
{
    public Guid ProductId { get; set; }
    public List<Guid> SideDishGroupIds { get; set; } = [];
}