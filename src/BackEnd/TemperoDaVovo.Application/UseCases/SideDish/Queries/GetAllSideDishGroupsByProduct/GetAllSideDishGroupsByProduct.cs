using TemperoDaVovo.Application.UseCases.SideDish.Queries.GetSideDishGroupsByProduct;
using TemperoDaVovo.Communications.Responses;
using TemperoDaVovo.Domain.Interfaces.ReadOnly;

namespace TemperoDaVovo.Application.UseCases.SideDish.Queries.GetAllSideDishGroupsByProduct;

public class GetAllSideDishGroupsByProduct : IGetAllSideDishGroupsByProduct
{

    private IProductSideDishGroupReadOnlyRepository _productSideDishGroupReadOnlyRepository;

    public GetAllSideDishGroupsByProduct(IProductSideDishGroupReadOnlyRepository productSideDishGroupReadOnlyRepository)
    {
        _productSideDishGroupReadOnlyRepository = productSideDishGroupReadOnlyRepository;
    }

    public async Task<List<GetAllSideDishGroupsResponse>> Execute(Guid productId)
    {
        var productGroup = await _productSideDishGroupReadOnlyRepository.GetAllProductSideDishGroupsAsync(productId);
        
        return productGroup.Select(pg => new GetAllSideDishGroupsResponse()
        {
            Id = pg.SideDishGroup.Id,
            Name = pg.SideDishGroup.Name,
            MinQuantity = pg.SideDishGroup.MinQuantity,
            MaxQuantity = pg.SideDishGroup.MaxQuantity,
            IsRequired = pg.SideDishGroup.IsRequired,
            SideDish = pg.SideDishGroup.SideDish.Select(sd => new SideDishResponseJson()
            {
                Id = sd.Id,
                Name = sd.Name,
                UnitPrice = sd.UnitPrice,
                IsActive = sd.IsActive,
            }).ToList()
        }).ToList();
        
    }
}