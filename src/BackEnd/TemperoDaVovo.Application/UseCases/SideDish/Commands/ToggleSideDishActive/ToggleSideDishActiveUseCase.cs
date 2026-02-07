using TemperoDaVovo.Communications.Responses;
using TemperoDaVovo.Domain.Interfaces.ReadOnly;
using TemperoDaVovo.Domain.Interfaces.WriteOnly;

namespace TemperoDaVovo.Application.UseCases.SideDish.Commands.ToggleSideDishActive;

public class ToggleSideDishActiveUseCase : IToggleSideDishActiveUseCase
{
    
    private readonly ISideDishReadOnlyRepository _sideDishReadOnlyRepository;
    private readonly ISideDishWriteOnlyRepository _sideDishWriteOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ToggleSideDishActiveUseCase(ISideDishReadOnlyRepository sideDishReadOnlyRepository, ISideDishWriteOnlyRepository sideDishWriteOnlyRepository, IUnitOfWork unitOfWork)
    {
        _sideDishReadOnlyRepository = sideDishReadOnlyRepository;
        _sideDishWriteOnlyRepository = sideDishWriteOnlyRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ToggleSideDishActiveResponseJson> Execute(Guid sideDishId, bool isActive)
    {
        var sideDish = await _sideDishReadOnlyRepository.GetSideDishById(sideDishId);

        if (sideDish == null)
        {
            throw new KeyNotFoundException($"Produto com ID {sideDishId} não encontrado.");
        }
        
        sideDish.IsActive = isActive;
    
        await _sideDishWriteOnlyRepository.ToggleActive(sideDish);
        await _unitOfWork.CommitAsync();

        return new ToggleSideDishActiveResponseJson()
        {
            Id = sideDish.Id,
            Name = sideDish.Name,
            IsActive = sideDish.IsActive,
            Message = sideDish.IsActive ? "Produto ativado com sucesso" : "Produto pausado com sucesso"
        };
    }
}