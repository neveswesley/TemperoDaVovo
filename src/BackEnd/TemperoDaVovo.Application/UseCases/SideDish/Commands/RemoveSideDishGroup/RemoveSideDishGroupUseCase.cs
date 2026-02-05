using TemperoDaVovo.Domain.Interfaces.WriteOnly;

namespace TemperoDaVovo.Application.UseCases.SideDish.Commands.RemoveSideDishGroup;

public class RemoveSideDishGroupUseCase : IRemoveSideDishGroupUseCase
{
    
    private readonly ISideDishWriteOnlyRepository _sideDishWriteOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveSideDishGroupUseCase(ISideDishWriteOnlyRepository sideDishWriteOnlyRepository, IUnitOfWork unitOfWork)
    {
        _sideDishWriteOnlyRepository = sideDishWriteOnlyRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(Guid productId, List<Guid> sideDishGroupIds)
    {
        await _sideDishWriteOnlyRepository.RemoveSideDishGroupsAsync(productId, sideDishGroupIds);
        await _unitOfWork.CommitAsync();
    }
}