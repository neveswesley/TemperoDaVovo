using TemperoDaVovo.Application.Interfaces;
using TemperoDaVovo.Communications.Responses;
using TemperoDaVovo.Domain.Interfaces.ReadOnly;
using TemperoDaVovo.Exceptions.ExceptionsBase;

namespace TemperoDaVovo.Application.UseCases.User.Queries.Get;

public class GetUserUseCase : IGetUserUseCase
{
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;
    private readonly IAuthorizationService _authorizationService;

    public GetUserUseCase(IUserReadOnlyRepository userReadOnlyRepository, IAuthorizationService authorizationService)
    {
        _userReadOnlyRepository = userReadOnlyRepository;
        _authorizationService = authorizationService;
    }

    public async Task<GetUserResponse> ExecuteAsync(Guid userId)
    {
        var user = await _userReadOnlyRepository.GetByIdAsync(userId);
        
        _authorizationService.ValidateUserOwnership(userId);

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