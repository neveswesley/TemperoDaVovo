namespace TemperoDaVovo.Communications.Responses;

public class CityResponseJson
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public List<NeighborhoodResponseJson> Neighborhoods{ get; set; } =
        new List<NeighborhoodResponseJson>();
}