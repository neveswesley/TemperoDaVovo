using TemperoDaVovo.Communications.Requests;

namespace TemperoDaVovo.Communications.Responses;

public class DuplicateProductResponseJson
{
    
    
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public ProductRequestJson DuplicatedProduct { get; set; } = new ProductRequestJson();
}