using TemperoDaVovo.Domain.Entities;

namespace TemperoDaVovo.Communications.Responses;

public class ProductResponseJson
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string? ImageUrl { get; set; }
    public bool IsActive { get; set; }
    public CategoryResponseJson? Category { get; set; }  // ← CORRETO: é objeto
}