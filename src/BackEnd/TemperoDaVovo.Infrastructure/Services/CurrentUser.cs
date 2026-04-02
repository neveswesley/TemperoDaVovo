using Microsoft.AspNetCore.Http;
using TemperoDaVovo.Application.Interfaces;
using TemperoDaVovo.Exceptions.ExceptionsBase;
using System.Security.Claims;

namespace TemperoDaVovo.Infrastructure.Services;

public class CurrentUser : ICurrentUser
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUser(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    private ClaimsPrincipal? User =>
        _httpContextAccessor.HttpContext?.User;

    public bool IsAuthenticated =>
        User?.Identity?.IsAuthenticated == true;

    public Guid? UserId
    {
        get
        {
            var value = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Guid.TryParse(value, out var id) ? id : null;
        }
    }

    public Guid? RestaurantId
    {
        get
        {
            var value = User?.FindFirst("restaurantId")?.Value;
            return Guid.TryParse(value, out var id) ? id : null;
        }
    }

    public string? Role =>
        User?.FindFirst(ClaimTypes.Role)?.Value;

    public void EnsureAuthenticated()
    {
        if (!IsAuthenticated)
            throw new UnauthorizedException(["Not authorized."]);
    }
}