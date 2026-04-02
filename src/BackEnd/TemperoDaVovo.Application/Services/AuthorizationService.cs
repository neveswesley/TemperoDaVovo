using TemperoDaVovo.Application.Interfaces;
using TemperoDaVovo.Exceptions.ExceptionsBase;

namespace TemperoDaVovo.Application.Services;

public class AuthorizationService : IAuthorizationService
{
    private readonly ICurrentUser _currentUser;

    public AuthorizationService(ICurrentUser currentUser)
    {
        _currentUser = currentUser;
    }

    public void ValidateRestaurantOwnership(Guid restaurantId)
    {
        if (restaurantId != _currentUser.RestaurantId)
            throw new ForbiddenException(["You are not authorized to perform this action."]);
    }

    public void ValidateUserOwnership(Guid userId)
    {
        if (_currentUser.UserId != userId)
            throw new ForbiddenException(["You are not authorized to perform this action."]);
    }
}