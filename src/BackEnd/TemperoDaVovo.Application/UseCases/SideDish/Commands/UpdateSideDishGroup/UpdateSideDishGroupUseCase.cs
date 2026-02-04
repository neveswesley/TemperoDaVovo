using TemperoDaVovo.Communications.Requests;
using TemperoDaVovo.Domain.Interfaces.ReadOnly;
using TemperoDaVovo.Domain.Interfaces.WriteOnly;

namespace TemperoDaVovo.Application.UseCases.SideDish.Commands.UpdateSideDishGroup;

public class UpdateSideDishGroupUseCase : IUpdateSideDishGroupUseCase
{
    
    private readonly ISideDishReadOnlyRepository _sideDishReadOnlyRepository;
    private readonly ISideDishWriteOnlyRepository _sideDishWriteOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateSideDishGroupUseCase(ISideDishReadOnlyRepository sideDishReadOnlyRepository, ISideDishWriteOnlyRepository sideDishWriteOnlyRepository, IUnitOfWork unitOfWork)
    {
        _sideDishReadOnlyRepository = sideDishReadOnlyRepository;
        _sideDishWriteOnlyRepository = sideDishWriteOnlyRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Execute(UpdateSideDishGroupJson request, Guid sideDishGroupId)
    {
        var sideDishGroup = await _sideDishReadOnlyRepository.GetSideDishGroupById(sideDishGroupId);
        sideDishGroup.Name = request.Name;
        sideDishGroup.MinQuantity = request.MinQuantity;
        sideDishGroup.MaxQuantity = request.MaxQuantity;
        
        await _sideDishWriteOnlyRepository.UpdateSideDishGroup(sideDishGroup);
        await _unitOfWork.CommitAsync();
        
        return sideDishGroup.Id;
    }
}