using TemperoDaVovo.Domain.Entities;

namespace TemperoDaVovo.Communications.Responses;

public class CreateSideDishGroupResponseJson
{
    public Guid Id { get; set; }
    public Guid RestaurantId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int MinQuantity { get; set; }
    public int MaxQuantity { get; set; }
}