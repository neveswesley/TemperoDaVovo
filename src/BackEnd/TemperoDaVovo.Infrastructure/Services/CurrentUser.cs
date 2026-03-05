using Microsoft.AspNetCore.Http;
using TemperoDaVovo.Application.Interfaces;

namespace TemperoDaVovo.Infrastructure.Services;

using System.Security.Claims;

public class CurrentUser : ICurrentUser
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUser(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public bool IsAuthenticated =>
        _httpContextAccessor.HttpContext?.User.Identity?.IsAuthenticated == true;

    public Guid? RestaurantId
    {
        get
        {
            var value = _httpContextAccessor.HttpContext?
                .User
                .FindFirst("restaurantId")?.Value;

            return Guid.TryParse(value, out var id) ? id : null;
        }
    }
}