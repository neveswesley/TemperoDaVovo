namespace TemperoDaVovo.Communications.Responses;

public class SideDishGroupResponseJson
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsRequired { get; set; }
    public int MinQuantity { get; set; }
    public int MaxQuantity { get; set; }
    public bool IsPaused { get; set; }
    public List<SideDishResponseJson> SideDish { get; set; } = new();
}