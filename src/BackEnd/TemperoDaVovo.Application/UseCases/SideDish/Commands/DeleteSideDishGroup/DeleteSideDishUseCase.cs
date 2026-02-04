using TemperoDaVovo.Domain.Interfaces.ReadOnly;
using TemperoDaVovo.Domain.Interfaces.WriteOnly;

namespace TemperoDaVovo.Application.UseCases.SideDish.Commands.DeleteSideDishGroup;

public class DeleteSideDishUseCase : IDeleteSideDishUseCase
{
    
    private readonly ISideDishWriteOnlyRepository _sideDishWriteOnlyRepository;
    private readonly ISideDishReadOnlyRepository _sideDishReadOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteSideDishUseCase(ISideDishWriteOnlyRepository sideDishWriteOnlyRepository, ISideDishReadOnlyRepository sideDishReadOnlyRepository, IUnitOfWork unitOfWork)
    {
        _sideDishWriteOnlyRepository = sideDishWriteOnlyRepository;
        _sideDishReadOnlyRepository = sideDishReadOnlyRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(Guid sideDishGroupId)
    {
        var sideDishGroup = await _sideDishReadOnlyRepository.GetSideDishGroupById(sideDishGroupId);
        await _sideDishWriteOnlyRepository.DeleteSideDishGroup(sideDishGroup);
        await _unitOfWork.CommitAsync();
    }
}