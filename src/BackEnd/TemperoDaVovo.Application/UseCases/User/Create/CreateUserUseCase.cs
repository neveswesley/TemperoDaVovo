using TemperoDaVovo.Communications.Requests;
using TemperoDaVovo.Communications.Responses;
using TemperoDaVovo.Domain.Interfaces.ReadOnly;
using TemperoDaVovo.Domain.Interfaces.WriteOnly;
using TemperoDaVovo.Exceptions.ExceptionsBase;

namespace TemperoDaVovo.Application.UseCases.User.Create;

public class CreateUserUseCase : ICreateUserUseCase
{
    private readonly IUserWriteOnlyRepository _userWriteOnlyRepository;
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateUserUseCase(IUserWriteOnlyRepository userWriteOnlyRepository, IUserReadOnlyRepository userReadOnlyRepository, IUnitOfWork unitOfWork)
    {
        _userWriteOnlyRepository = userWriteOnlyRepository;
        _userReadOnlyRepository = userReadOnlyRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CreateUserResponseJson> Execute(CreateUserRequestJson request)
    {
        await Validate(request);
        var user = new Domain.Entities.User()
        {
            RestaurantId = request.RestaurantId,
            Email = request.Email,
            PasswordHash = request.Password
        };

        await _userWriteOnlyRepository.RegisterUser(user);
        await _unitOfWork.Commit();

        return new CreateUserResponseJson()
        {
            Email = user.Email
        };
    }
    private async Task Validate(CreateUserRequestJson request)
    {
        var validator = new CreateUserValidator();
        var result = await validator.ValidateAsync(request);

        if (await _userReadOnlyRepository.EmailExists(request.Email))
            result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, "Este e-mail já está sendo utilizado."));
        
        if (await _userReadOnlyRepository.RestaurantHasUser(request.RestaurantId))
            result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, "Restaurante já cadastrado."));
        
        // restaurante já tem login

        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }
}