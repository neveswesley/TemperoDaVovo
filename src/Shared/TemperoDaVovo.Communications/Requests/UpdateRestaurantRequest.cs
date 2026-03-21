using TemperoDaVovo.Domain.Entities;
using TemperoDaVovo.Domain.Enums;

namespace TemperoDaVovo.Communications.Requests;

public class UpdateRestaurantRequest
{
    public string Name { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string? Description { get; set; }
    public Address Address { get; set; }
    public RestaurantCategory RestaurantCategory { get; set; }
}