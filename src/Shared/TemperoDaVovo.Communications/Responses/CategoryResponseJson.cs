namespace TemperoDaVovo.Communications.Responses;

public class CategoryResponseJson
{
    public Guid Id { get; set; }
    public Guid CategoryId { get; set; }
    public string CategoryName { get; set; } = string.Empty;
}