namespace TemperoDaVovo.Domain.Entities;

public class Neighborhood
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Name { get; private set; }
    public int BaseDeliveryTimeInMinutes { get; private set; }
    public decimal? DeliveryFee { get; private set; }
    public bool IsActive { get; private set; }

    // cidade
    public Guid CityId { get; set; }
    public City City { get; set; }

    private Neighborhood() { }

    public Neighborhood(string name, decimal? deliveryFee, Guid cityId, int baseDeliveryTimeInMinutes)
    {
        if (deliveryFee.HasValue && deliveryFee < 0)
            throw new ArgumentException("Taxa de entrega deve ser maior que 0");

        Name = name;
        DeliveryFee = deliveryFee;
        CityId = cityId;
        BaseDeliveryTimeInMinutes = baseDeliveryTimeInMinutes;
        IsActive = true;
    }

    public void UpdateFee(decimal newFee)
    {
        if (newFee < 0)
            throw new ArgumentException("Taxa de entrega deve ser maior que 0");

        DeliveryFee = newFee;
    }

    public void UpdateName(string newName)
    {
        if (string.IsNullOrEmpty(newName))
            throw new ArgumentException("O nome da cidade não pode ser vazio.");

        Name = newName;
    }

    public void UpdateBaseDeliveryTimeInMinutes(int newBaseDeliveryTimeInMinutes)
    {
        if (newBaseDeliveryTimeInMinutes < 0)
            throw new ArgumentException("Taxa de entrega deve ser maior que 0");
        BaseDeliveryTimeInMinutes = newBaseDeliveryTimeInMinutes;
    }

    public void Deactivate()
    {
        IsActive = false;
    }
}