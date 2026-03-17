using TemperoDaVovo.Domain.Entities;

namespace TemperoDaVovo.Domain.Services;

public interface IRestaurantScheduleService
{
    bool IsOpenNow(IEnumerable<RestaurantOpeningHour> openingHours, DateTime now);
}