using Microsoft.AspNetCore.Http;
using TemperoDaVovo.Domain.Interfaces.ReadOnly;
using TemperoDaVovo.Domain.Interfaces.WriteOnly;
using TemperoDaVovo.Exceptions.ExceptionsBase;

namespace TemperoDaVovo.Application.UseCases.Product.Commands.UpdateImage;

public class UpdateProductImageUseCase : IUpdateProductImageUseCase
{
    private readonly IProductReadOnlyRepository _productReadOnlyRepository;
    private readonly IProductWriteOnlyRepository _productWriteOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateProductImageUseCase(IProductReadOnlyRepository productReadOnlyRepository, IProductWriteOnlyRepository productWriteOnlyRepository, IUnitOfWork unitOfWork)
    {
        _productReadOnlyRepository = productReadOnlyRepository;
        _productWriteOnlyRepository = productWriteOnlyRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(Guid productId, IFormFile file)
    {
        var product = await _productReadOnlyRepository.GetProductByIdWithCategory(productId);

        if (product == null)
            throw new BusinessException(["Produto não encontrado"]);

        if (file == null || file.Length == 0)
            throw new BusinessException(["Arquivo inválido"]);

        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "uploads");

        if (!Directory.Exists(uploadsFolder))
            Directory.CreateDirectory(uploadsFolder);

        if (!string.IsNullOrEmpty(product.ImageUrl))
        {
            var oldFilePath = Path.Combine(
                uploadsFolder,
                Path.GetFileName(product.ImageUrl)
            );

            if (File.Exists(oldFilePath))
                File.Delete(oldFilePath);
        }

        var fileName = $"{Guid.NewGuid()}_{file.FileName}";
        var filePath = Path.Combine(uploadsFolder, fileName);

        using var stream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(stream);

        product.ImageUrl = $"/uploads/{fileName}";

        await _productWriteOnlyRepository.UpdateProduct(product);
        await _unitOfWork.Commit();
    }

}