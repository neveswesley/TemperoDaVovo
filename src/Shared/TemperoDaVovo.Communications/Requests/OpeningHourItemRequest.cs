namespace TemperoDaVovo.Communications.Requests;

public class OpeningHourItemRequest
{
    public DayOfWeek DayOfWeek { get; set; }
    public string OpenTime { get; set; } = string.Empty;
    public string CloseTime { get; set; } = string.Empty;
}