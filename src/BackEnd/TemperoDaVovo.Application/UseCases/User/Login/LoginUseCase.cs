using TemperoDaVovo.Communications.Requests;
using TemperoDaVovo.Communications.Responses;
using TemperoDaVovo.Domain.Interfaces;
using TemperoDaVovo.Domain.Interfaces.ReadOnly;
using TemperoDaVovo.Domain.Interfaces.WriteOnly;
using TemperoDaVovo.Exceptions.ExceptionsBase;

namespace TemperoDaVovo.Application.UseCases.User.Login;

public class LoginUseCase : ILoginUseCase
{
    
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;
    private readonly IPasswordHasher _passwordHasher;

    public LoginUseCase(IUserReadOnlyRepository userReadOnlyRepository, IPasswordHasher passwordHasher)
    {
        _userReadOnlyRepository = userReadOnlyRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<LoginUserResponseJson> Execute(LoginUserRequestJson request)
    {
        Validate(request);
        
        var user = await _userReadOnlyRepository.GetByEmail(request.Email);

        if (user == null ||
            !_passwordHasher.Verify(request.Password, user.PasswordHash))
        {
            throw new UnauthorizedException(new List<string>
            { "Email ou senha inválidos." });
        }

        return new LoginUserResponseJson()
        {
            UserId = user.Id,
            RestaurantId = user.RestaurantId,
            Success = true
        };
    }

    private static void Validate(LoginUserRequestJson request)
    {
        if (string.IsNullOrWhiteSpace(request.Email) ||
            string.IsNullOrWhiteSpace(request.Password))
        {
            throw new ErrorOnValidationException(
                new List<string> { "Email e senha são obrigatórios." }
            );
        }
    }

}