using TemperoDaVovo.Communications.Responses;
using TemperoDaVovo.Domain.Interfaces.ReadOnly;

namespace TemperoDaVovo.Application.UseCases.Product.Queries.GetById;

public class GetProductByIdUseCase : IGetProductByIdUseCase
{
    private readonly IProductReadOnlyRepository _productReadOnlyRepository;

    public GetProductByIdUseCase(IProductReadOnlyRepository productReadOnlyRepository)
    {
        _productReadOnlyRepository = productReadOnlyRepository;
    }

    public async Task<GetProductWithSideDishesResponseJson> Execute(Guid productId)
    {
        var product = await _productReadOnlyRepository.GetProductByIdWithCategory(productId);

        if (product == null)
            throw new KeyNotFoundException("Produto não encontrado.");

        var result = new GetProductWithSideDishesResponseJson
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            ImageUrl = product.ImageUrl,
            Category = new CategoryResponseJson()
            {
                Id = product.Category.Id,
                CategoryName = product.Category.Name
            },
            ProductSideDishGroups = product.ProductSideDishGroups
                .Select(psdg => new ProductSideDishGroupResponseJson
                {
                    Id = psdg.Id,
                    ProductId = psdg.ProductId,
                    SideDishGroupId = psdg.SideDishGroupId,
                    IsRequired = psdg.IsRequired,
                    SideDishGroup = new SideDishGroupResponseJson
                    {
                        Id = psdg.SideDishGroup.Id,
                        Name = psdg.SideDishGroup.Name,
                        IsRequired = psdg.SideDishGroup.IsRequired,
                        MinQuantity = psdg.SideDishGroup.MinQuantity,
                        MaxQuantity = psdg.SideDishGroup.MaxQuantity,
                        IsPaused = psdg.SideDishGroup.IsPaused,
                        SideDish = psdg.SideDishGroup.SideDish
                            .Select(sd => new SideDishResponseJson
                            {
                                Id = sd.Id,
                                Name = sd.Name,
                                Quantity = sd.Quantity,
                                UnitPrice = sd.UnitPrice
                            })
                            .ToList()
                    }
                })
                .ToList()
        };

        return result;  // ⬅️ ESTAVA FALTANDO O RETURN!
    }
}