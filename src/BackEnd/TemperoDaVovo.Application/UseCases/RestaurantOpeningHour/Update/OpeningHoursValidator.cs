namespace TemperoDaVovo.Application.UseCases.RestaurantOpeningHour.OpeningHours;

public static class OpeningHoursValidator
{
    public static void Validate(List<Domain.Entities.RestaurantOpeningHour> hours)
    {
        var groupedByDay = hours
            .GroupBy(x => x.DayOfWeek);

        foreach (var group in groupedByDay)
        {
            var ordered = group
                .OrderBy(x => x.OpenTime)
                .ToList();

            for (int i = 0; i < ordered.Count - 1; i++)
            {
                var current = ordered[i];
                var next = ordered[i + 1];

                if (current.CloseTime > next.OpenTime)
                    throw new ArgumentException($"Existem horários sobrepostos em {group.Key}.");
            }
        }
    }
}