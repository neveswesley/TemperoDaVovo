namespace TemperoDaVovo.Communications.Responses;

public class GetProductWithSideDishesResponseJson
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string? ImageUrl { get; set; }
    public CategoryResponseJson Category { get; set; }
    public List<ProductSideDishGroupResponseJson> ProductSideDishGroups { get; set; } = [];

}