using TemperoDaVovo.Application.Interfaces;
using TemperoDaVovo.Domain.Interfaces.ReadOnly;
using TemperoDaVovo.Domain.Interfaces.WriteOnly;
using TemperoDaVovo.Exceptions.ExceptionsBase;

namespace TemperoDaVovo.Application.UseCases.Product.Commands.Delete;

public class DeleteProductUseCase : IDeleteProductUseCase
{
    private readonly IProductWriteOnlyRepository _productWriteOnlyRepository;
    private readonly IProductReadOnlyRepository _productReadOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAuthorizationService _authorizationService;

    public DeleteProductUseCase(IProductWriteOnlyRepository productWriteOnlyRepository,
        IProductReadOnlyRepository productReadOnlyRepository, IUnitOfWork unitOfWork,
        IAuthorizationService authorizationService)
    {
        _productWriteOnlyRepository = productWriteOnlyRepository;
        _productReadOnlyRepository = productReadOnlyRepository;
        _unitOfWork = unitOfWork;
        _authorizationService = authorizationService;
    }

    public async Task ExecuteAsync(Guid productId)
    {
        var product = await _productReadOnlyRepository.GetProductByIdWithCategory(productId);
        if (product == null)
            throw new NotFoundException(["Product not found."]);
        
        _authorizationService.ValidateRestaurantOwnership(product.RestaurantId);
        
        _productWriteOnlyRepository.DeleteProduct(productId);
        await _unitOfWork.CommitAsync();
    }
}