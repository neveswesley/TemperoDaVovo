namespace TemperoDaVovo.Domain.Entities;

public class Category : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public Guid RestaurantId { get; set; }
    public ICollection<Product> Products { get; set; } = new List<Product>();

    public void UpdateName(string newName)
    {
        Name = newName;
    }
}