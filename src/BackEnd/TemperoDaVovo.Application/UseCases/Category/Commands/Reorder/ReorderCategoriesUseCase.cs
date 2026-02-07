using TemperoDaVovo.Communications.Requests;
using TemperoDaVovo.Communications.Responses;
using TemperoDaVovo.Domain.Interfaces.ReadOnly;
using TemperoDaVovo.Domain.Interfaces.WriteOnly;

namespace TemperoDaVovo.Application.UseCases.Category.Commands.Reorder;

public class ReorderCategoriesUseCase : IReorderCategoriesUseCase
{
    private readonly ICategoryReadOnlyRepository _categoryReadOnlyRepository;
    private readonly ICategoryWriteOnlyRepository _categoryWriteOnlyRepository;

    public ReorderCategoriesUseCase(ICategoryReadOnlyRepository categoryReadOnlyRepository,
        ICategoryWriteOnlyRepository categoryWriteOnlyRepository)
    {
        _categoryReadOnlyRepository = categoryReadOnlyRepository;
        _categoryWriteOnlyRepository = categoryWriteOnlyRepository;
    }

    public async Task<ReorderCategoriesResponseJson> ExecuteAsync(ReorderCategoriesRequest request)
    {
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