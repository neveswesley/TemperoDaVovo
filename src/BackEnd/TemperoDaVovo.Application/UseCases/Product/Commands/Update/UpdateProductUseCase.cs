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

    public async Task Execute(UpdateProductRequest request, Guid productId)
    {
        var product = await _productReadOnlyRepository.GetProductByIdWithCategory(productId);

        if (product == null)
            throw new BusinessException(["Produto não encontrado"]);

        product.Name = request.Name;
        product.Description = request.Description;
        product.Price = request.Price;

        if (request.CategoryId.HasValue)
            product.CategoryId = request.CategoryId.Value;

        await _productWriteOnlyRepository.UpdateProduct(product);
        await _unitOfWork.Commit();
    }
}