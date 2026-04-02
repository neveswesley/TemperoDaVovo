using TemperoDaVovo.Communications.Requests;
using TemperoDaVovo.Domain.Interfaces;
using TemperoDaVovo.Domain.Interfaces.ReadOnly;
using TemperoDaVovo.Domain.Interfaces.WriteOnly;
using TemperoDaVovo.Exceptions.ExceptionsBase;

namespace TemperoDaVovo.Application.UseCases.User.Commands.UpdatePassword;

public class UpdatePasswordUseCase : IUpdatePasswordUseCase
{
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;
    private readonly IUserWriteOnlyRepository _userWriteOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;

    public UpdatePasswordUseCase(
        IUserReadOnlyRepository userReadOnlyRepository,
        IUserWriteOnlyRepository userWriteOnlyRepository,
        IUnitOfWork unitOfWork,
        IPasswordHasher passwordHasher)
    {
        _userReadOnlyRepository = userReadOnlyRepository;
        _userWriteOnlyRepository = userWriteOnlyRepository;
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
    }

    public async Task ExecuteAsync(Guid userId, UpdatePasswordRequest request)
    {
        Validate(request);

        var user = await _userReadOnlyRepository.GetByIdAsync(userId);

        if (user is null)
            throw new NotFoundException(["Usuário não encontrado."]);

        user.UpdatePassword(
            request.CurrentPassword,
            request.NewPassword,
            _passwordHasher);

        _userWriteOnlyRepository.Update(user);
        await _unitOfWork.CommitAsync();
    }

    private static void Validate(UpdatePasswordRequest request)
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(request.CurrentPassword))
            errors.Add("A senha atual é obrigatória.");

        if (string.IsNullOrWhiteSpace(request.NewPassword))
            errors.Add("A nova senha é obrigatória.");

        if (request.NewPassword?.Length < 6)
            errors.Add("A nova senha deve ter pelo menos 6 caracteres.");

        if (!string.Equals(request.NewPassword, request.ConfirmPassword))
            errors.Add("As senhas não coincidem.");

        if (errors.Count > 0)
            throw new ErrorOnValidationException(errors);
    }
}