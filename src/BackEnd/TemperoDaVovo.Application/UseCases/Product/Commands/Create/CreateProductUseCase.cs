using Microsoft.AspNetCore.Http;
using TemperoDaVovo.Communications.Requests;
using TemperoDaVovo.Communications.Responses;
using TemperoDaVovo.Domain.Interfaces.ReadOnly;
using TemperoDaVovo.Domain.Interfaces.WriteOnly;
using TemperoDaVovo.Exceptions.ExceptionsBase;

namespace TemperoDaVovo.Application.UseCases.Product.Commands.Create;

public class CreateProductUseCase : ICreateProductUseCase
{
    private readonly IProductWriteOnlyRepository _productWriteOnlyRepository;
    private readonly IRestaurantReadOnlyRepository _restaurantReadOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateProductUseCase(IProductWriteOnlyRepository productWriteOnlyRepository, IRestaurantReadOnlyRepository restaurantReadOnlyRepository, IUnitOfWork unitOfWork)
    {
        _productWriteOnlyRepository = productWriteOnlyRepository;
        _restaurantReadOnlyRepository = restaurantReadOnlyRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CreateProductResponseJson> Execute(CreateProductRequestJson request, IFormFile file)
    {
        
        if (file != null)
        {
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var fileName = $"{Guid.NewGuid()}_{file.FileName}";
            var filePath = Path.Combine(uploadsFolder, fileName);

            using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);

            request.ImageUrl = $"https://meusite.com/uploads/{fileName}";

        }

        var restaurant = await _restaurantReadOnlyRepository.RestaurantExists(request.RestaurantId);

        if (restaurant == null)
            throw new BusinessException([ "Restaurante não encontrado" ]);
        
        var product = new Domain.Entities.Product
        {
            RestaurantId = request.RestaurantId,
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            Category = request.ProductType,
            ImageUrl = request.ImageUrl,
        };
        
        await _productWriteOnlyRepository.CreateProduct(product);
        await _unitOfWork.Commit();

        return new CreateProductResponseJson()
        {
            Id = product.Id
        };
    }
}