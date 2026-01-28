using Microsoft.AspNetCore.Http;

namespace TemperoDaVovo.Communications.Requests;

public class UpdateProductImageRequest
{
    public IFormFile Image { get; set; }
}