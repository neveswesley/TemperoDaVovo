// Application/UseCases/Product/Commands/Delete/DeleteProductUseCase.cs

using TemperoDaVovo.Domain.Interfaces.WriteOnly;

namespace TemperoDaVovo.Application.UseCases.Product.Commands.Delete;

public class DeleteProductUseCase : IDeleteProductUseCase
{
    private readonly IProductWriteOnlyRepository _write;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteProductUseCase(IProductWriteOnlyRepository write, IUnitOfWork unitOfWork)
    {
        _write = write;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(Guid productId)
    {
        await _write.DeleteProduct(productId);
        await _unitOfWork.Commit();
    }
}