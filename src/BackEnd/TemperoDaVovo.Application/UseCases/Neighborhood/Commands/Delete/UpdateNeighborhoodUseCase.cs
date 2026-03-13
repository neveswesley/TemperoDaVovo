using TemperoDaVovo.Communications.Requests;
using TemperoDaVovo.Domain.Interfaces.ReadOnly;
using TemperoDaVovo.Domain.Interfaces.WriteOnly;
using TemperoDaVovo.Exceptions.ExceptionsBase;

namespace TemperoDaVovo.Application.UseCases.Neighborhood.Commands.Delete;

public class UpdateNeighborhoodUseCase : IUpdateNeighborhoodUseCase
{
    private readonly INeighborhoodReadOnlyRepository _neighborhoodReadOnlyRepository;
    private readonly INeighborhoodWriteOnlyRepository _neighborhoodWriteOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateNeighborhoodUseCase(INeighborhoodReadOnlyRepository neighborhoodReadOnlyRepository,
        INeighborhoodWriteOnlyRepository neighborhoodWriteOnlyRepository, IUnitOfWork unitOfWork)
    {
        _neighborhoodReadOnlyRepository = neighborhoodReadOnlyRepository;
        _neighborhoodWriteOnlyRepository = neighborhoodWriteOnlyRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task ExecuteAsync(Guid neighborhoodId, UpdateNeighborhoodRequestJson request)
    {
        var neighborhood = await _neighborhoodReadOnlyRepository.GetNeighborhoodById(neighborhoodId);
        if (neighborhood == null)
            throw new NotFoundException(["Bairro não encontrado."]);

        neighborhood.UpdateName(request.Name);
        neighborhood.UpdateFee(request.DeliveryFee);
        neighborhood.UpdateBaseDeliveryTimeInMinutes(request.BaseDeliveryTimeInMinutes);
        
        _neighborhoodWriteOnlyRepository.UpdateAsync(neighborhood);
        await _unitOfWork.CommitAsync();
    }
}