// Application/UseCases/Product/Commands/Delete/DeleteProductUseCase.cs

using TemperoDaVovo.Domain.Interfaces.WriteOnly;

namespace TemperoDaVovo.Application.UseCases.Product.Commands.Delete;

public class DeleteProductUseCase : IDeleteProductUseCase
{
    private readonly IProductWriteOnlyRepository _productWriteOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteProductUseCase(IProductWriteOnlyRepository productWriteOnlyRepository, IUnitOfWork unitOfWork)
    {
        _productWriteOnlyRepository = productWriteOnlyRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(Guid productId)
    {
        _productWriteOnlyRepository.DeleteProduct(productId);
        await _unitOfWork.CommitAsync();
    }
}