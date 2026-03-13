using TemperoDaVovo.Domain.Interfaces.ReadOnly;
using TemperoDaVovo.Domain.Interfaces.WriteOnly;
using TemperoDaVovo.Exceptions.ExceptionsBase;

namespace TemperoDaVovo.Application.UseCases.Neighborhood.Commands.Update;

public class DeleteNeighborhoodUseCase : IDeleteNeighborhoodUseCase
{
    
    private readonly INeighborhoodReadOnlyRepository  _neighborhoodReadOnlyRepository;
    private readonly INeighborhoodWriteOnlyRepository _neighborhoodWriteOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteNeighborhoodUseCase(INeighborhoodReadOnlyRepository neighborhoodReadOnlyRepository, INeighborhoodWriteOnlyRepository neighborhoodWriteOnlyRepository, IUnitOfWork unitOfWork)
    {
        _neighborhoodReadOnlyRepository = neighborhoodReadOnlyRepository;
        _neighborhoodWriteOnlyRepository = neighborhoodWriteOnlyRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task ExecuteAsync(Guid neighborhoodId)
    {
        var neighborhood = await _neighborhoodReadOnlyRepository.GetNeighborhoodById(neighborhoodId);
        if (neighborhood == null)
            throw new NotFoundException(["Neighborhood not found."]);

        neighborhood.Deactivate();
        
        _neighborhoodWriteOnlyRepository.UpdateAsync(neighborhood);
        await _unitOfWork.CommitAsync();
    }
}