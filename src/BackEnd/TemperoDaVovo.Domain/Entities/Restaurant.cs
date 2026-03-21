using TemperoDaVovo.Domain.Enums;
using TemperoDaVovo.Exceptions.ExceptionsBase;

namespace TemperoDaVovo.Domain.Entities;

public class Restaurant : BaseEntity
{
    public string Name { get; private set; }
    public string Phone { get; private set; }
    public Address? Address { get; private set; }
    public string? Description { get; private set; }
    public DateTime LastNameUpdateAt { get; set; }
    public RestaurantCategory? RestaurantCategory { get; private set; }
    public int GlobalAdditionalDeliveryMinutes { get; private set; }
    public List<PaymentWay> PaymentWays { get; private set; } = [];
    public ICollection<RestaurantOpeningHour> OpeningHours { get; private set; } = new List<RestaurantOpeningHour>();
    public ICollection<Neighborhood> Neighborhoods { get; set; } = new List<Neighborhood>();

    private Restaurant()
    { }
    
    public Restaurant(string name, string phone, Address? address, string? description, RestaurantCategory? restaurantCategory)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("O nome do restaurante não pode estar vazio.");

        if (string.IsNullOrWhiteSpace(phone))
            throw new ArgumentException("O número de telefone não pode estar vazio.");


        Name = name;
        Phone = phone;
        Address = address;
        Description = description;
        RestaurantCategory = restaurantCategory;
    }
    
    public void UpdateGlobalDeliveryDelay(int minutes)
    {
        if (minutes < 0)
            throw new ArgumentException("O tempo de atraso não pode ser negativo.");
        
        GlobalAdditionalDeliveryMinutes = minutes;
    }

    public void UpdateName(string name)
    {
        if (Name.Trim().ToLower() == name.Trim().ToLower())
            return;
        
        var today = DateTime.UtcNow.Date;
        var lastUpdate = LastNameUpdateAt.Date;

        var daysPassed = (today - lastUpdate).Days;

        if (daysPassed < 60)
        {
            var daysRemaining = 60 - daysPassed;
            throw new BusinessException([$"Você só pode alterar o nome em {daysRemaining} dias."]);
        }

        Name = name;
        LastNameUpdateAt = DateTime.UtcNow;
    }

    public void UpdateDescription(string? description)
    {
        description = string.IsNullOrWhiteSpace(description) ? null : description.Trim();
        
        if (Description == description)
            return;
        
        Description = description;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateRestaurantCategory(RestaurantCategory? restaurantCategory)
    {
        
        RestaurantCategory = restaurantCategory;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateAddress(Address? address)
    {
        Address = address;
        UpdatedAt = DateTime.UtcNow;
    }

    public void SetPaymentWay(List<PaymentWay> paymentWays)
    {
        PaymentWays = paymentWays ?? throw new ArgumentNullException(nameof(paymentWays));
        
        var distinctPaymentsWays = paymentWays
            .Distinct()
            .ToList();

        if (distinctPaymentsWays.Count == 0)
            throw new BusinessException(["O restaurante deve possuir ao menos uma forma de pagamento"]);
        
        UpdatedAt = DateTime.UtcNow;
    }

}