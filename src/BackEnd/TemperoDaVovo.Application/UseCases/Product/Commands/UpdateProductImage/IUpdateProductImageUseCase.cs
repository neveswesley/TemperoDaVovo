using Microsoft.AspNetCore.Http;

namespace TemperoDaVovo.Application.UseCases.Product.Commands.UpdateImage;

public interface IUpdateProductImageUseCase
{
    Task Execute(Guid productId, IFormFile file);
}