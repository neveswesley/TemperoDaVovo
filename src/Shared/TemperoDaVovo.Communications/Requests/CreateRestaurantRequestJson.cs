using TemperoDaVovo.Domain.Entities;

namespace TemperoDaVovo.Communications.Requests;

public class CreateRestaurantRequestJson
{
    public string Name { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public AddressRequest Address { get; set; } =  new AddressRequest();
}