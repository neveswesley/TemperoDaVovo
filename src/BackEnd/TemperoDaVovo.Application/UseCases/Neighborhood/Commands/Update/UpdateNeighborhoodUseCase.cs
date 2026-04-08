using TemperoDaVovo.Application.Interfaces;
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
    private readonly IAuthorizationService _authorizationService;

    public UpdateNeighborhoodUseCase(INeighborhoodReadOnlyRepository neighborhoodReadOnlyRepository, INeighborhoodWriteOnlyRepository neighborhoodWriteOnlyRepository, IUnitOfWork unitOfWork, IAuthorizationService authorizationService)
    {
        _neighborhoodReadOnlyRepository = neighborhoodReadOnlyRepository;
        _neighborhoodWriteOnlyRepository = neighborhoodWriteOnlyRepository;
        _unitOfWork = unitOfWork;
        _authorizationService = authorizationService;
    }

    public async Task ExecuteAsync(Guid neighborhoodId, UpdateNeighborhoodRequestJson request)
    {
        var neighborhood = await _neighborhoodReadOnlyRepository.GetNeighborhoodById(neighborhoodId);
        if (neighborhood == null)
            throw new NotFoundException(["Bairro não encontrado."]);
        
        _authorizationService.ValidateRestaurantOwnership(neighborhood.City.RestaurantId);

        neighborhood.UpdateName(request.Name);
        neighborhood.UpdateFee(request.DeliveryFee);
        neighborhood.UpdateBaseDeliveryTimeInMinutes(request.BaseDeliveryTimeInMinutes);
        
        _neighborhoodWriteOnlyRepository.UpdateAsync(neighborhood);
        await _unitOfWork.CommitAsync();
    }
}