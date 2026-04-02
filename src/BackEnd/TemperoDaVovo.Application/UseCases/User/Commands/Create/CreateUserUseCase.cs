using TemperoDaVovo.Application.UseCases.User.Create;
using TemperoDaVovo.Communications.Requests;
using TemperoDaVovo.Communications.Responses;
using TemperoDaVovo.Domain.Entities;
using TemperoDaVovo.Domain.Interfaces;
using TemperoDaVovo.Domain.Interfaces.ReadOnly;
using TemperoDaVovo.Domain.Interfaces.WriteOnly;
using TemperoDaVovo.Exceptions.ExceptionsBase;

namespace TemperoDaVovo.Application.UseCases.User.Commands.Create;

public class CreateUserUseCase : ICreateUserUseCase
{
    private readonly IUserWriteOnlyRepository _userWriteOnlyRepository;
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IVerificationCodeWriteOnlyRepository _verificationCodeWriteOnlyRepository;
    private readonly IEmailService _emailService;

    public CreateUserUseCase(IUserWriteOnlyRepository userWriteOnlyRepository, IUserReadOnlyRepository userReadOnlyRepository, IPasswordHasher passwordHasher, IUnitOfWork unitOfWork, IVerificationCodeWriteOnlyRepository verificationCodeWriteOnlyRepository, IEmailService emailService)
    {
        _userWriteOnlyRepository = userWriteOnlyRepository;
        _userReadOnlyRepository = userReadOnlyRepository;
        _passwordHasher = passwordHasher;
        _unitOfWork = unitOfWork;
        _verificationCodeWriteOnlyRepository = verificationCodeWriteOnlyRepository;
        _emailService = emailService;
    }

    public async Task<CreateUserResponseJson> ExecuteAsync(CreateUserRequestJson request)
    {
        await Validate(request);
        
        var user = new Domain.Entities.User()
        {
            RestaurantId = request.RestaurantId,
            Email = request.Email,
            PasswordHash = request.Password
        };
        
        user.PasswordHash = _passwordHasher.Hash(request.Password);

        await _userWriteOnlyRepository.RegisterUser(user);
        await _unitOfWork.CommitAsync();
        
        var code = new Random().Next(100000, 999999).ToString();

        await _verificationCodeWriteOnlyRepository.AddAsync(new VerificationCode
        {
            UserId = user.Id,
            Code = code,
            Type = VerificationCodeType.EmailConfirmation,
            ExpiresAt = DateTime.UtcNow.AddMinutes(10)
        });

        await _unitOfWork.CommitAsync();
        await _emailService.SendVerificationCodeAsync(user.Email, code, VerificationCodeType.EmailConfirmation);

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
        
        
        if (await _userReadOnlyRepository.RestaurantHasAnyUser(request.RestaurantId))
            result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, "Restaurante já cadastrado."));

        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }
}