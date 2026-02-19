using TemperoDaVovo.Domain.Interfaces.ReadOnly;
using TemperoDaVovo.Domain.Interfaces.WriteOnly;
using TemperoDaVovo.Exceptions.ExceptionsBase;

namespace TemperoDaVovo.Application.UseCases.SideDish.Commands.DeleteGroup;

public class DeleteSideDishGroupUseCase : IDeleteSideDishGroupUseCase
{
    
    private readonly ISideDishWriteOnlyRepository _sideDishWriteOnlyRepository;
    private readonly ISideDishReadOnlyRepository _sideDishReadOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteSideDishGroupUseCase(ISideDishWriteOnlyRepository sideDishWriteOnlyRepository, ISideDishReadOnlyRepository sideDishReadOnlyRepository, IUnitOfWork unitOfWork)
    {
        _sideDishWriteOnlyRepository = sideDishWriteOnlyRepository;
        _sideDishReadOnlyRepository = sideDishReadOnlyRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(Guid groupId)
    {
        var sideDish = await _sideDishReadOnlyRepository.GetSideDishGroupById(groupId);
        
        if (sideDish is null)
            throw new NotFoundException(["Complemento não encontrado"]);
        
        await _sideDishWriteOnlyRepository.DeleteGroupAsync(groupId);
        
        await _unitOfWork.CommitAsync();
    }
}