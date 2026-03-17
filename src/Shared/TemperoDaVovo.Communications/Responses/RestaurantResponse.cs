using TemperoDaVovo.Domain.Entities;
using TemperoDaVovo.Domain.Enums;

namespace TemperoDaVovo.Communications.Responses;

public class RestaurantResponse
{
    public string Name { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string? Description { get; set; }
    public RestaurantCategory? RestaurantCategory { get; set; }

    public Address? Address { get; set; }
}