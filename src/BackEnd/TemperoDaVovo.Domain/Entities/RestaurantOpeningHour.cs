namespace TemperoDaVovo.Domain.Entities;

public class RestaurantOpeningHour
{
    public Guid Id { get; private set; }
    public Guid RestaurantId { get; private set; }
    public DayOfWeek DayOfWeek { get; private set; }
    public DateTime OpenTime { get; private set; }
    public DateTime CloseTime { get; private set; }

    public Restaurant Restaurant { get; set; } = null!;

    public RestaurantOpeningHour(
        Guid restaurantId,
        DayOfWeek dayOfWeek,
        DateTime openTime,
        DateTime closeTime)
    {
        if (openTime >= closeTime)
            throw new ArgumentException("O horário de abertura deve ser menor que o horário de fechamento.");

        RestaurantId = restaurantId;
        DayOfWeek = dayOfWeek;
        OpenTime = openTime;
        CloseTime = closeTime;
    }

    public void Update(DateTime openTime, DateTime closeTime)
    {
        if (openTime >= closeTime)
            throw new ArgumentException("O horário de abertura deve ser menor que o horário de fechamento.");

        OpenTime = openTime;
        CloseTime = closeTime;
    }
}