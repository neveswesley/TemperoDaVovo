using TemperoDaVovo.Application.Services;
using TemperoDaVovo.Communications.Responses;
using TemperoDaVovo.Domain.Entities;
using TemperoDaVovo.Domain.Interfaces.ReadOnly;
using TemperoDaVovo.Domain.Interfaces.WriteOnly;
using TemperoDaVovo.Exceptions.ExceptionsBase;

namespace TemperoDaVovo.Application.UseCases.User.Commands.VerifyTwoFactor;

public class VerifyTwoFactorUseCase : IVerifyTwoFactorUseCase
{
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;
    private readonly IVerificationCodeReadOnlyRepository _verificationCodeReadOnlyRepository;
    private readonly IJwtService _jwtTokenGenerator;
    private readonly IUnitOfWork _unitOfWork;

    public VerifyTwoFactorUseCase(
        IUserReadOnlyRepository userReadOnlyRepository,
        IVerificationCodeReadOnlyRepository verificationCodeReadOnlyRepository,
        IJwtService jwtTokenGenerator,
        IUnitOfWork unitOfWork)
    {
        _userReadOnlyRepository = userReadOnlyRepository;
        _verificationCodeReadOnlyRepository = verificationCodeReadOnlyRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
        _unitOfWork = unitOfWork;
    }

    public async Task<LoginUserResponseJson> ExecuteAsync(string email, string code)
    {
        var user = await _userReadOnlyRepository.GetByEmail(email)
                   ?? throw new ErrorOnValidationException(["Usuário não encontrado"]);

        var verification = await _verificationCodeReadOnlyRepository
                               .GetActiveCodeAsync(user.Id, VerificationCodeType.TwoFactor)
                           ?? throw new ErrorOnValidationException(["Nenhum código ativo encontrado"]);

        if (!verification.IsValid())
            throw new ErrorOnValidationException(["Código expirado ou inválido"]);

        if (verification.Code != code)
        {
            verification.Attempts++;
            await _unitOfWork.CommitAsync();
            throw new ErrorOnValidationException(["Código incorreto"]);
        }

        verification.IsUsed = true;
        await _unitOfWork.CommitAsync();

        var token = _jwtTokenGenerator.Generate(user.Id, user.RestaurantId);
        return new LoginUserResponseJson { Token = token };
    }
}