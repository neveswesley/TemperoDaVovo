namespace TemperoDaVovo.Domain.Entities;

public class SideDishGroup : BaseEntity
{
    public Guid RestaurantId { get; set; }
    public string Name { get; set; } = string.Empty;
    public ICollection<SideDish> SideDish { get; set; } = [];
    public bool IsRequired { get; set; }
    public int MinQuantity { get; set; }
    public int MaxQuantity { get; set; }
}