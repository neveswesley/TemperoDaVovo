using TemperoDaVovo.Application.UseCases.User.Get;
using TemperoDaVovo.Communications.Responses;
using TemperoDaVovo.Domain.Interfaces.ReadOnly;
using TemperoDaVovo.Exceptions.ExceptionsBase;

namespace TemperoDaVovo.Application.UseCases.User.Queries.Get;

public class GetUserUseCase : IGetUserUseCase
{
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;

    public GetUserUseCase(IUserReadOnlyRepository userReadOnlyRepository)
    {
        _userReadOnlyRepository = userReadOnlyRepository;
    }

    public async Task<GetUserResponse> ExecuteAsync(Guid userId)
    {
        var user = await _userReadOnlyRepository.GetByIdAsync(userId);

        if (user is null)
            throw new NotFoundException(["Usuário não encontrado."]);

        return new GetUserResponse
        {
            Id = user.Id,
            Email = user.Email,
            Role = user.Role.ToString(),
        };
    }
}