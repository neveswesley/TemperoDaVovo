using Microsoft.AspNetCore.Http;
using TemperoDaVovo.Communications.Requests;

namespace TemperoDaVovo.Application.UseCases.Product.Commands.Update;

public interface IUpdateProductUseCase
{
    Task Execute(UpdateProductRequest request, Guid productId);
}