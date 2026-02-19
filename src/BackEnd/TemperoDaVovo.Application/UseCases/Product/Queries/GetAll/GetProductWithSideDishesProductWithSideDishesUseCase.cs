using TemperoDaVovo.Communications.Responses;
using TemperoDaVovo.Domain.Interfaces.ReadOnly;

namespace TemperoDaVovo.Application.UseCases.Product.Queries.GetAll;

public class GetProductWithSideDishesProductWithSideDishesUseCase : IGetProductWithSideDishesUseCase
{
    private readonly IProductReadOnlyRepository _productReadOnlyRepository;

    public GetProductWithSideDishesProductWithSideDishesUseCase(IProductReadOnlyRepository productReadOnlyRepository)
    {
        _productReadOnlyRepository = productReadOnlyRepository;
    }

    public async Task<List<GetProductWithSideDishesResponseJson>> Execute(Guid restaurantId, string? search)
    {
        var products = await _productReadOnlyRepository.GetAllProductByRestaurantWithSideDish(restaurantId, search);

        var result = products.Select(p => new GetProductWithSideDishesResponseJson
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            Price = p.Price,
            ImageUrl = p.ImageUrl,
            Category = new CategoryResponseJson()
            {
                Id = p.Category.Id,
                CategoryName = p.Category.Name
            },
            ProductSideDishGroups = p.ProductSideDishGroups
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
        }).ToList();

        return result;
    }
}