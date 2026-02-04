using TemperoDaVovo.Communications.Requests;
using TemperoDaVovo.Domain.Entities;
using TemperoDaVovo.Domain.Interfaces.ReadOnly;
using TemperoDaVovo.Domain.Interfaces.WriteOnly;
using TemperoDaVovo.Exceptions.ExceptionsBase;

namespace TemperoDaVovo.Application.UseCases.SideDish.Commands.LinkGroup;

public class LinkSideDishSideDishSideDishSideDishGroupsToProductsToProductsToProductsToProductUseCase : ILinkSideDishSideDishGroupsToProductsToProductUseCase
{

    private readonly IProductReadOnlyRepository _productRepository;
    private readonly ISideDishReadOnlyRepository _sideDishReadOnlyRepository;
    private readonly IProductSideDishGroupWriteOnlyRepository _linkRepository;
    private readonly IUnitOfWork _unitOfWork;

    public LinkSideDishSideDishSideDishSideDishGroupsToProductsToProductsToProductsToProductUseCase(IProductReadOnlyRepository productRepository,
        ISideDishReadOnlyRepository sideDishReadOnlyRepository, IProductSideDishGroupWriteOnlyRepository linkRepository,
        IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _sideDishReadOnlyRepository = sideDishReadOnlyRepository;
        _linkRepository = linkRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(LinkSideDishGroupsToProductRequest request)
    {
        var product = await _productRepository.GetProductByIdWithCategory(request.ProductId);
        if (product is null)
            throw new NotFoundException(["Produto não encontrado"]);

        var groups = await _sideDishReadOnlyRepository
            .GetByIdsAsync(request.SideDishGroupIds);

        if (groups.Count != request.SideDishGroupIds.Count)
            throw new Exception("Um ou mais grupos de complemento são inválidos");

        var invalidGroup = groups
            .Any(g => g.RestaurantId != product.RestaurantId);

        if (invalidGroup)
            throw new Exception("Grupo de complemento não pertence ao restaurante do produto");

        var alreadyLinkedGroupIds =
            await _linkRepository.GetLinkedGroupIdsAsync(product.Id);

        var groupsToLink = groups
            .Where(g => !alreadyLinkedGroupIds.Contains(g.Id))
            .ToList();

        foreach (var group in groupsToLink)
        {
            var link = new ProductSideDishGroup(
                product.Id,
                group.Id
            );

            await _linkRepository.AddAsync(link);
        }

        await _unitOfWork.CommitAsync();
    }
}