namespace TemperoDaVovo.Communications.Requests;

public class UpdateRestaurantOpeningHoursRequest
{
    public List<OpeningHourItemRequest> OpeningHours { get; set; } = [];
}