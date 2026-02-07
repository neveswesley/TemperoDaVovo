using TemperoDaVovo.Communications.Requests;

namespace TemperoDaVovo.Communications.Responses;

public class ReorderCategoriesResponseJson
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public List<CategoryOrderRequestJson> UpdatedCategories { get; set; }
}