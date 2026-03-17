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
    public ICollection<RestaurantOpeningHour> OpeningHours { get; private set; } = new List<RestaurantOpeningHour>();
    public ICollection<Neighborhood> Neighborhoods { get; set; } = new List<Neighborhood>();

    private Restaurant()
    { }
    
    public Restaurant(string name, string phone, Address address)
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

        var lastUpdated = LastNameUpdateAt; // 16/03
        var futureDate = lastUpdated.AddDays(60); // 16/05
        var diasFaltantes = futureDate.Date.Day - lastUpdated.Day;

        // 25/03
        if (DateTime.UtcNow > futureDate)
        {
            
        }


    }

    public void UpdateDescription(string description)
    {
        
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

}