using Microsoft.AspNetCore.Http;
using TemperoDaVovo.Communications.Requests;
using TemperoDaVovo.Communications.Responses;

namespace TemperoDaVovo.Application.UseCases.Product.Commands.Create;

public interface ICreateProductUseCase
{
    Task<CreateProductResponseJson> Execute(CreateProductRequestJson request, IFormFile file);
}