using TemperoDaVovo.Domain.Enums;

namespace TemperoDaVovo.Communications.Requests;

public class UpdateRestaurantRequest
{
    public string Name { get; set; }
    public string Phone { get; set; }
    public AddressRequest AddressRequest { get; set; }
    public RestaurantCategory RestaurantCategory { get; set; }
}