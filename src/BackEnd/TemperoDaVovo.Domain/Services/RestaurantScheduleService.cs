using TemperoDaVovo.Domain.Entities;

namespace TemperoDaVovo.Domain.Services;

public class RestaurantScheduleService : IRestaurantScheduleService
{
    public bool IsOpenNow(IEnumerable<RestaurantOpeningHour> openingHours, DateTime now)
    {
        var currentDay = now.DayOfWeek;
        var currentTime = new TimeSpan(now.Hour, now.Minute, now.Second);

        return openingHours.Any(x =>
            x.DayOfWeek == currentDay &&
            currentTime >= ConvertToTimeSpan(x.OpenTime) &&
            currentTime < ConvertToTimeSpan(x.CloseTime));
    }
    
    private TimeSpan ConvertToTimeSpan(DateTime dt)
    {
        return new TimeSpan(dt.Hour, dt.Minute, dt.Second);
    }
}