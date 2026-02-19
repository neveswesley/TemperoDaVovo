using TemperoDaVovo.Communications.Requests;
using TemperoDaVovo.Communications.Responses;

namespace TemperoDaVovo.Application.UseCases.SideDishGroup.Commands;

public interface ICreateSideDishGroupUseCase
{
    Task<CreateSideDishGroupResponseJson> Execute(CreateSideDishGroupRequestJson request);
}