using TemperoDaVovo.Communications.Requests;
using TemperoDaVovo.Domain.Interfaces.ReadOnly;
using TemperoDaVovo.Domain.Interfaces.WriteOnly;
using TemperoDaVovo.Exceptions.ExceptionsBase;

namespace TemperoDaVovo.Application.UseCases.Product.Commands.Update;

public class UpdateProductUseCase : IUpdateProductUseCase
{
    private readonly IProductReadOnlyRepository _productReadOnlyRepository;
    private readonly IProductWriteOnlyRepository _productWriteOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateProductUseCase(
        IProductReadOnlyRepository productReadOnlyRepository,
        IProductWriteOnlyRepository productWriteOnlyRepository,
        IUnitOfWork unitOfWork)
    {
        _productReadOnlyRepository = productReadOnlyRepository;
        _productWriteOnlyRepository = productWriteOnlyRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(UpdateProductRequestJson request, Guid productId)
    {
        var product = await _productReadOnlyRepository.GetProductByIdWithCategory(productId);

        if (product == null)
            throw new BusinessException(["Produto não encontrado"]);

        product.UpdateName(request.Name);
        if (request.Description != null) product.UpdateDescription(request.Description);
        product.UpdatePrice(request.Price);

        if (request.CategoryId.HasValue)
            product.CategoryId = request.CategoryId.Value;

        await _productWriteOnlyRepository.UpdateProduct(product);
        await _unitOfWork.CommitAsync();
    }
}