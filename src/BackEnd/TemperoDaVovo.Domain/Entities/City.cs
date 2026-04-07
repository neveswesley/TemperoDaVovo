namespace TemperoDaVovo.Domain.Entities;

public class City
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public bool IsActive { get; private set; }

    //bairros
    public ICollection<Neighborhood> Neighborhoods { get; private set; } = new List<Neighborhood>();
    
    //restaurante
    public Guid RestaurantId { get; private set; }
    public Restaurant Restaurant { get; private set; } = null!;
    protected City(){}

    public City(string name, Guid restaurantId)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("O nome da cidade não pode ser vazio.");
        
        Name = name;
        RestaurantId = restaurantId;
        IsActive = true;
    }

    public void UpdateName(string name)
    {
        Name = name;
    }
    
    public void Deactivate()
    {
        IsActive = false;
    }
    
}