namespace TemperoDaVovo.Communications.Requests;

public class UpdateSideDishGroupJson
{
    public string Name { get; set; } = string.Empty;
    public int MinQuantity { get; set; }
    public int MaxQuantity { get; set; }
}