using TemperoDaVovo.Application.Interfaces;
using TemperoDaVovo.Domain.Interfaces.ReadOnly;
using TemperoDaVovo.Domain.Interfaces.WriteOnly;
using TemperoDaVovo.Exceptions.ExceptionsBase;

namespace TemperoDaVovo.Application.UseCases.Product.Commands.RemoveImage;

public class RemoveProductImageUseCase : IRemoveProductImageUseCase
{
    private readonly IProductReadOnlyRepository _productReadOnlyRepository;
    private readonly IProductWriteOnlyRepository _productWriteOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAuthorizationService _authorizationService;

    public RemoveProductImageUseCase(IProductReadOnlyRepository productReadOnlyRepository, IProductWriteOnlyRepository productWriteOnlyRepository, IUnitOfWork unitOfWork, IAuthorizationService authorizationService)
    {
        _productReadOnlyRepository = productReadOnlyRepository;
        _productWriteOnlyRepository = productWriteOnlyRepository;
        _unitOfWork = unitOfWork;
        _authorizationService = authorizationService;
    }

    public async Task ExecuteAsync(Guid productId)
    {
        var product = await _productReadOnlyRepository.GetProductByIdWithCategory(productId);

        if (product == null)
            throw new BusinessException(["Produto não encontrado"]);

        _authorizationService.ValidateRestaurantOwnership(product.RestaurantId);

        if (!string.IsNullOrEmpty(product.ImageUrl))
        {
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
            var oldFilePath = Path.Combine(uploadsFolder, Path.GetFileName(product.ImageUrl));

            if (File.Exists(oldFilePath))
                File.Delete(oldFilePath);

            product.ImageUrl = null;

            await _productWriteOnlyRepository.UpdateProduct(product);
            await _unitOfWork.CommitAsync();
        }
    }
}