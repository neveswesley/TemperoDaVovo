using TemperoDaVovo.Domain.Interfaces.ReadOnly;
using TemperoDaVovo.Domain.Interfaces.WriteOnly;

namespace TemperoDaVovo.Application.UseCases.SideDish.Commands.UpdateSideDish;

public class UpdateSideDishUseCase : IUpdateSideDishUseCase
{
    
    private readonly ISideDishReadOnlyRepository _sideDishReadOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateSideDishUseCase(ISideDishReadOnlyRepository sideDishReadOnlyRepository, IUnitOfWork unitOfWork)
    {
        _sideDishReadOnlyRepository = sideDishReadOnlyRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(Guid sideDishId, string name, int quantity, decimal price)
    {
        var sideDish = await _sideDishReadOnlyRepository.GetSideDishById(sideDishId);
        sideDish.Update(name, quantity, price);

        await _unitOfWork.CommitAsync();
    }
}