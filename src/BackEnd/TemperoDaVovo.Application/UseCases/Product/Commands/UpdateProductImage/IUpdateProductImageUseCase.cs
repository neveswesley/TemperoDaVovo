using Microsoft.AspNetCore.Http;

namespace TemperoDaVovo.Application.UseCases.Product.Commands.UpdateProductImage;

public interface IUpdateProductImageUseCase
{
    Task ExecuteAsync(Guid productId, IFormFile file);
}