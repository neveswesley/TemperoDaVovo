using TemperoDaVovo.Communications.Requests;
using TemperoDaVovo.Communications.Responses;
using TemperoDaVovo.Domain.Interfaces.ReadOnly;
using TemperoDaVovo.Domain.Interfaces.WriteOnly;
using TemperoDaVovo.Exceptions.ExceptionsBase;

namespace TemperoDaVovo.Application.UseCases.Restaurant.Create;

public class CreateRestaurantUseCase : ICreateRestaurantUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRestaurantWriteOnlyRepository _restaurantWriteOnlyRepository;
    private readonly IRestaurantReadOnlyRepository _restaurantReadOnlyRepository;

    public CreateRestaurantUseCase(IUnitOfWork unitOfWork, IRestaurantWriteOnlyRepository restaurantWriteOnlyRepository,
        IRestaurantReadOnlyRepository restaurantReadOnlyRepository)
    {
        _unitOfWork = unitOfWork;
        _restaurantWriteOnlyRepository = restaurantWriteOnlyRepository;
        _restaurantReadOnlyRepository = restaurantReadOnlyRepository;
    }

    public async Task<CreateRestaurantResponseJson> Execute(CreateRestaurantRequestJson request)
    {
        await Validate(request);
        var restaurant = new Domain.Entities.Restaurant()
        {
            Name = request.Name,
            Address = request.Address,
            Phone = request.Phone
        };

        await _restaurantWriteOnlyRepository.AddAsync(restaurant);
        await _unitOfWork.Commit();

        return new CreateRestaurantResponseJson
        {
            Id = restaurant.Id,
            Name = restaurant.Name,
        };
    }

    private async Task Validate(CreateRestaurantRequestJson request)
    {
        var validator = new CreateRestaurantValidator();
        var result = await validator.ValidateAsync(request);

        var phoneExist = await _restaurantReadOnlyRepository.PhoneExists(request.Phone);
        if (phoneExist)
            result.Errors.Add(
                new FluentValidation.Results.ValidationFailure(string.Empty, "Número de telefone já cadastrado."));

        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }
}