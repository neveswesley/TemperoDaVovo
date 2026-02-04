using Microsoft.AspNetCore.Http;

namespace TemperoDaVovo.Communications.Requests;

public class UpdateProductImageRequestJson
{
    public IFormFile Image { get; set; }
}