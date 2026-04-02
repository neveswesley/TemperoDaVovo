using TemperoDaVovo.Application.Interfaces;
using TemperoDaVovo.Communications.Responses;
using TemperoDaVovo.Domain.Interfaces.ReadOnly;
using TemperoDaVovo.Domain.Interfaces.WriteOnly;

namespace TemperoDaVovo.Application.UseCases.Product.Commands.ToggleProductActive;

public class ToggleProductActiveUseCase : IToggleProductActiveUseCase
{
    private readonly IProductWriteOnlyRepository _productWriteOnlyRepository;
    private readonly IProductReadOnlyRepository _productReadOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAuthorizationService _authorizationService;

    public ToggleProductActiveUseCase(IProductWriteOnlyRepository productWriteOnlyRepository, IProductReadOnlyRepository productReadOnlyRepository, IUnitOfWork unitOfWork, IAuthorizationService authorizationService)
    {
        _productWriteOnlyRepository = productWriteOnlyRepository;
        _productReadOnlyRepository = productReadOnlyRepository;
        _unitOfWork = unitOfWork;
        _authorizationService = authorizationService;
    }

    public async Task<ToggleProductActiveResponseJson> ExecuteAsync(Guid productId, bool isPaused)
    {
        var product = await _productReadOnlyRepository.GetProductByIdWithCategory(productId);

        if (product == null)
            throw new KeyNotFoundException($"Produto com ID {productId} não encontrado.");
        
        _authorizationService.ValidateRestaurantOwnership(product.RestaurantId);

        
        product.IsPaused = isPaused;
    
        await _productWriteOnlyRepository.ToggleActive(product);
        await _unitOfWork.CommitAsync();

        return new ToggleProductActiveResponseJson()
        {
            Id = product.Id,
            Name = product.Name,
            IsPaused = product.IsPaused,
            Message = product.IsActive ? "Produto ativado com sucesso" : "Produto pausado com sucesso"
        };
    }
}