using TemperoDaVovo.Application.UseCases.RestaurantOpeningHour.OpeningHours;
using TemperoDaVovo.Communications.Requests;
using TemperoDaVovo.Domain.Interfaces.ReadOnly;
using TemperoDaVovo.Domain.Interfaces.WriteOnly;
using TemperoDaVovo.Exceptions.ExceptionsBase;

namespace TemperoDaVovo.Application.UseCases.RestaurantOpeningHour.Update;

public class OpeningHoursUseCase : IOpeningHoursUseCase
{
    
    private readonly IRestaurantReadOnlyRepository _restaurantReadOnlyRepository;
    private readonly IRestaurantWriteOnlyRepository _restaurantWriteOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public OpeningHoursUseCase(IRestaurantReadOnlyRepository restaurantReadOnlyRepository, IRestaurantWriteOnlyRepository restaurantWriteOnlyRepository, IUnitOfWork unitOfWork)
    {
        _restaurantReadOnlyRepository = restaurantReadOnlyRepository;
        _restaurantWriteOnlyRepository = restaurantWriteOnlyRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(Guid restaurantId, UpdateRestaurantOpeningHoursRequest request)
    {
        var restaurant = await _restaurantReadOnlyRepository.GetByIdWithOpeningHours(restaurantId);

        if (restaurant == null)
            throw new NotFoundException(["Restaurante não encontrado."]);

        var newHours = request.OpeningHours.
            Select(x => new Domain.Entities.RestaurantOpeningHour(
            restaurantId,
            x.DayOfWeek,
            DateTime.Parse(x.OpenTime),
            DateTime.Parse(x.CloseTime)
        )).ToList();

        OpeningHoursValidator.Validate(newHours);
        
        restaurant.OpeningHours.Clear();

        foreach (var hour in newHours)
            restaurant.OpeningHours.Add(hour);
        
        await _unitOfWork.CommitAsync();
    }
}