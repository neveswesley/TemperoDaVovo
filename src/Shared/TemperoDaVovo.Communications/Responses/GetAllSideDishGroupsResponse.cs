namespace TemperoDaVovo.Communications.Responses;

public class GetAllSideDishGroupsResponse
{
    public Guid Id { get; set; }
    public Guid RestaurantId { get; set; }
    public string Name { get; set; } = string.Empty;
    public ICollection<SideDishResponseJson> SideDish { get; set; } = [];
    public bool IsRequired  { get; set; }
    public int MinQuantity { get; set; }
    public int MaxQuantity { get; set; }
}