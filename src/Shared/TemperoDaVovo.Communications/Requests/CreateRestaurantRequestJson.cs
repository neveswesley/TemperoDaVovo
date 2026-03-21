using TemperoDaVovo.Domain.Entities;
using TemperoDaVovo.Domain.Enums;

namespace TemperoDaVovo.Communications.Requests;

public class CreateRestaurantRequestJson
{
    public string Name { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string? Description { get; set; }
    public List<PaymentWay> PaymentWays { get; set; } = [];
    public RestaurantCategory RestaurantCategory { get; set; }
    public AddressRequest Address { get; set; } =  new AddressRequest();
}