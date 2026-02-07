using TemperoDaVovo.Communications.Requests;
using TemperoDaVovo.Communications.Responses;
using TemperoDaVovo.Domain.Interfaces.ReadOnly;
using TemperoDaVovo.Domain.Interfaces.WriteOnly;
using TemperoDaVovo.Exceptions.ExceptionsBase;

namespace TemperoDaVovo.Application.UseCases.Product.Commands.Duplicate;

public class DuplicateProductUseCase : IDuplicateProductUseCase
{
    private readonly IProductReadOnlyRepository _productReadOnlyRepository;
    private readonly IProductWriteOnlyRepository _productWriteOnlyRepository;
    private readonly ISideDishReadOnlyRepository _sideDishReadOnlyRepository;
    private readonly ISideDishWriteOnlyRepository _sideDishWriteOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DuplicateProductUseCase(IProductReadOnlyRepository productReadOnlyRepository, IProductWriteOnlyRepository productWriteOnlyRepository, ISideDishReadOnlyRepository sideDishReadOnlyRepository, ISideDishWriteOnlyRepository sideDishWriteOnlyRepository, IUnitOfWork unitOfWork)
    {
        _productReadOnlyRepository = productReadOnlyRepository;
        _productWriteOnlyRepository = productWriteOnlyRepository;
        _sideDishReadOnlyRepository = sideDishReadOnlyRepository;
        _sideDishWriteOnlyRepository = sideDishWriteOnlyRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<DuplicateProductResponseJson> ExecuteAsync(DuplicateProductRequestJson request)
    {
        var originalProduct = await _productReadOnlyRepository.GetProductByIdWithCategory(request.ProductId);

        if (originalProduct == null)
            throw new NotFoundException(["Produto não encontrado"]);

        var newProductName = string.IsNullOrWhiteSpace(request.NewProductName)
            ? $"{originalProduct.Name} - Cópia"
            : request.NewProductName;

        var duplicatedProduct = new Domain.Entities.Product()
        {
            Id = Guid.NewGuid(),
            RestaurantId = originalProduct.RestaurantId,
            Name = newProductName,
            Description = originalProduct.Description,
            Price = originalProduct.Price,
            ImageUrl = originalProduct.ImageUrl,
            CategoryId = originalProduct.CategoryId,
            CreatedAt = DateTime.UtcNow,
        };

        var createdProduct = await _productWriteOnlyRepository.CreateProduct(duplicatedProduct);

        if (createdProduct == null)
            throw new DomainException(["Não foi possível duplicar o produto."]);


        var sideDishGroupsIds = await _sideDishReadOnlyRepository.GetSideDishesGroupIds(originalProduct.Id);

        if (sideDishGroupsIds.Any())
        {
            var linkedSuccess = await _sideDishWriteOnlyRepository
                .AddComplementGroupsToProductAsync(createdProduct.Id, sideDishGroupsIds);

            if (!linkedSuccess)
            {
                Console.WriteLine("Aviso: Produto duplicado, mas falhou ao vincular grupos de complementos");
            }
        }
        
        await _unitOfWork.CommitAsync();

        var completeProduct = await _productReadOnlyRepository.GetProductByIdWithCategory(createdProduct.Id);

        var productDto = new ProductRequest
        {
            Id = completeProduct.Id,
            Name = completeProduct.Name,
            Description = completeProduct.Description,
            Price = completeProduct.Price,
            ImageUrl = completeProduct.ImageUrl,
            IsActive = completeProduct.IsActive,
            CategoryId = completeProduct.CategoryId,
            ComplementGroups = completeProduct.ProductSideDishGroups?.Select(psg => new SideDishGroupRequest
            {
                Id = psg.SideDishGroup.Id,
                Name = psg.SideDishGroup.Name,
                MinQuantity = psg.SideDishGroup.MinQuantity,
                MaxQuantity = psg.SideDishGroup.MaxQuantity,
                IsRequired = psg.SideDishGroup.IsRequired
            }).ToList() ?? new List<SideDishGroupRequest>()
        };


        return new DuplicateProductResponseJson()
        {
            Success = true,
            Message = "Produto criado com sucesso.",
            DuplicatedProduct = productDto
        };
    }
}