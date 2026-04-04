using System.Globalization;
using Microsoft.AspNetCore.Http;
using TemperoDaVovo.Application.Interfaces;
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
    private readonly IAuthorizationService _authorizationService;

    public CreateProductUseCase(IProductWriteOnlyRepository productWriteOnlyRepository,
        IRestaurantReadOnlyRepository restaurantReadOnlyRepository, IUnitOfWork unitOfWork,
        IAuthorizationService authorizationService)
    {
        _productWriteOnlyRepository = productWriteOnlyRepository;
        _restaurantReadOnlyRepository = restaurantReadOnlyRepository;
        _unitOfWork = unitOfWork;
        _authorizationService = authorizationService;
    }

    public async Task<CreateProductResponseJson> ExecuteAsync(CreateProductRequestJson request, IFormFile file)
    {
        if (file != null)
        {
            var uploadsFolder = Path.Combine(AppContext.BaseDirectory, "uploads");

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var fileName = $"{Guid.NewGuid()}_{file.FileName}";
            var filePath = Path.Combine(uploadsFolder, fileName);

            using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);

            request.ImageUrl = $"/uploads/{fileName}";
        }

        var restaurant = await _restaurantReadOnlyRepository.RestaurantExists(request.RestaurantId);
        if (restaurant == null)
            throw new BusinessException(["Restaurante não encontrado"]);

        _authorizationService.ValidateRestaurantOwnership(request.RestaurantId);


        var product = new Domain.Entities.Product
        {
            RestaurantId = request.RestaurantId,
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            CategoryId = request.CategoryId,
            ImageUrl = request.ImageUrl,
            IsPaused = false
        };

        await _productWriteOnlyRepository.CreateProduct(product);
        await _unitOfWork.CommitAsync();

        return new CreateProductResponseJson()
        {
            Id = product.Id
        };
    }
}