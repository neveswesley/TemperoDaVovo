using TemperoDaVovo.Communications.Requests;
using TemperoDaVovo.Communications.Responses;

namespace TemperoDaVovo.Application.UseCases.SideDish.Commands.CreateSideDish;

public interface ICreateSideDishUseCase
{
    Task<SideDishResponseJson> Execute (CreateSideDishRequestJson request);
}