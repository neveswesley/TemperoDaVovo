namespace TemperoDaVovo.Communications.Responses;

public class GetAllProductsResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string? Category { get; set; }
    public bool IsActive { get; set; }
    public string? Description { get; set; }
}