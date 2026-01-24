namespace TemperoDaVovo.Communications.Responses;

public class CategoryWithProductsResponseJson
{
    public Guid CategoryId { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public List<ProductResponseJson> Products { get; set; } = [];
}