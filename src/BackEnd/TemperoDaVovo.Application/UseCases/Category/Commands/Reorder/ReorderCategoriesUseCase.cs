using TemperoDaVovo.Application.Interfaces;
using TemperoDaVovo.Communications.Requests;
using TemperoDaVovo.Communications.Responses;
using TemperoDaVovo.Domain.Interfaces.ReadOnly;
using TemperoDaVovo.Domain.Interfaces.WriteOnly;
using TemperoDaVovo.Exceptions.ExceptionsBase;

namespace TemperoDaVovo.Application.UseCases.Category.Commands.Reorder;

public class ReorderCategoriesUseCase : IReorderCategoriesUseCase
{
    private readonly ICategoryReadOnlyRepository _categoryReadOnlyRepository;
    private readonly ICategoryWriteOnlyRepository _categoryWriteOnlyRepository;
    private readonly IRestaurantReadOnlyRepository _restaurantReadOnlyRepository;
    private readonly IAuthorizationService _authorizationService;

    public ReorderCategoriesUseCase(ICategoryReadOnlyRepository categoryReadOnlyRepository, ICategoryWriteOnlyRepository categoryWriteOnlyRepository, IRestaurantReadOnlyRepository restaurantReadOnlyRepository, IAuthorizationService authorizationService)
    {
        _categoryReadOnlyRepository = categoryReadOnlyRepository;
        _categoryWriteOnlyRepository = categoryWriteOnlyRepository;
        _restaurantReadOnlyRepository = restaurantReadOnlyRepository;
        _authorizationService = authorizationService;
    }

    public async Task<ReorderCategoriesResponseJson> ExecuteAsync(ReorderCategoriesRequest request)
    {

        var restaurant = await _restaurantReadOnlyRepository.GetRestaurantById(request.RestaurantId);
        if (restaurant == null)
            throw new NotFoundException(["Restaurant not found."]);
 
        _authorizationService.ValidateRestaurantOwnership(request.RestaurantId);
        
        if (request.CategoryIds == null || !request.CategoryIds.Any())
        {
            return new ReorderCategoriesResponseJson
            {
                Success = false,
                Message = "Lista de categorias não pode estar vazia"
            };
        }

        if (request.CategoryIds.Count != request.CategoryIds.Distinct().Count())
        {
            return new ReorderCategoriesResponseJson
            {
                Success = false,
                Message = "IDs de categorias duplicados encontrados"
            };
        }

        var success = await _categoryWriteOnlyRepository.UpdateCategoryOrderAsync(
            request.RestaurantId,
            request.CategoryIds
        );

        if (!success)
        {
            return new ReorderCategoriesResponseJson
            {
                Success = false,
                Message = "Erro ao atualizar ordem das categorias"
            };
        }

        var updatedCategories = await _categoryReadOnlyRepository
            .GetCategoriesByRestaurantId(request.RestaurantId);

        var categoryOrderDtos = updatedCategories.Select(c => new CategoryOrderRequestJson
        {
            CategoryId = c.Id,
            CategoryName = c.Name,
            DisplayOrder = c.DisplayOrder
        }).ToList();

        return new ReorderCategoriesResponseJson
        {
            Success = true,
            Message = "Ordem das categorias atualizada com sucesso",
            UpdatedCategories = categoryOrderDtos
        };
    }
}