namespace TemperoDaVovo.Communications.Requests;

public class ProductRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public Guid CategoryId { get; set; }
    public List<SideDishGroupRequest> ComplementGroups { get; set; } = [];
}