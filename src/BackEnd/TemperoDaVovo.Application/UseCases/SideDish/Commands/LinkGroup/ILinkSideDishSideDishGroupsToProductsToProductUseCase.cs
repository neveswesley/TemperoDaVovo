using TemperoDaVovo.Communications.Requests;

namespace TemperoDaVovo.Application.UseCases.SideDish.Commands.LinkGroup;

public interface ILinkSideDishSideDishGroupsToProductsToProductUseCase
{
    Task Execute(LinkSideDishGroupsToProductRequest request);
}