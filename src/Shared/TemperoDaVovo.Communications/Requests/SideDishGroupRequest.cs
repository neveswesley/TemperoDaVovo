namespace TemperoDaVovo.Communications.Requests;

public class SideDishGroupRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int MinQuantity { get; set; }
    public int MaxQuantity { get; set; }
    public bool IsRequired { get; set; }
}