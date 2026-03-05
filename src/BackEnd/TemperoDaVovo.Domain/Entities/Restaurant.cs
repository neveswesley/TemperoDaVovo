namespace TemperoDaVovo.Domain.Entities;

public class Restaurant : BaseEntity
{
    public string Name { get; private set; }
    public string Phone { get; private set; }
    public string Address { get; private set; }
    public int GlobalAdditionalDeliveryMinutes { get; private set; }
    public ICollection<Neighborhood> Neighborhoods { get; set; } = new List<Neighborhood>();

    public Restaurant(string name, string phone, string address)
    {

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("O nome do restaurante não pode estar vazio.");
        
        Name = name;
        Phone = phone;
        Address = address;
    }
    
    public void UpdateGlobalDeliveryDelay(int minutes)
    {
        if (minutes < 0)
            throw new ArgumentException("O tempo de atraso não pode ser negativo.");
        
        GlobalAdditionalDeliveryMinutes = minutes;
    }

}