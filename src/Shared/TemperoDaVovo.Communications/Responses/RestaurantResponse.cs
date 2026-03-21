using TemperoDaVovo.Domain.Entities;
using TemperoDaVovo.Domain.Enums;

namespace TemperoDaVovo.Communications.Responses;

public class RestaurantResponse
{
    public string Name { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int? RestaurantCategory { get; set; }
    public ICollection<OpeningHourResponse> OpeningHours { get; set; } = new List<OpeningHourResponse>();
    public IList<PaymentWay> PaymentWays { get; set; } = new List<PaymentWay>();
    public AddressResponse? Address { get; set; }
}