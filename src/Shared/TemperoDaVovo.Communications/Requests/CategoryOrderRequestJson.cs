namespace TemperoDaVovo.Communications.Requests;

public class CategoryOrderRequestJson
{
    public Guid CategoryId { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public int DisplayOrder { get; set; }
}