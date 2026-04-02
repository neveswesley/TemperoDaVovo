using TemperoDaVovo.Domain.Entities;
using TemperoDaVovo.Domain.Interfaces.ReadOnly;
using TemperoDaVovo.Domain.Interfaces.WriteOnly;
using TemperoDaVovo.Exceptions.ExceptionsBase;

namespace TemperoDaVovo.Application.UseCases.User.Commands.ConfirmEmail;

// ConfirmEmailUseCase.cs
public class ConfirmEmailUseCase : IConfirmEmailUseCase
{
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;
    private readonly IVerificationCodeReadOnlyRepository _verificationCodeReadOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ConfirmEmailUseCase(
        IUserReadOnlyRepository userReadOnlyRepository,
        IVerificationCodeReadOnlyRepository verificationCodeReadOnlyRepository,
        IUnitOfWork unitOfWork)
    {
        _userReadOnlyRepository = userReadOnlyRepository;
        _verificationCodeReadOnlyRepository = verificationCodeReadOnlyRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task ExecuteAsync(string email, string code)
    {
        var user = await _userReadOnlyRepository.GetByEmail(email)
                   ?? throw new ErrorOnValidationException(["Usuário não encontrado"]);

        var verification = await _verificationCodeReadOnlyRepository
                               .GetActiveCodeAsync(user.Id, VerificationCodeType.EmailConfirmation)
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
        user.IsEmailConfirmed = true;

        await _unitOfWork.CommitAsync();
    }
}