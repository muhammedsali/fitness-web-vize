using GymMembershipSystem.Models;

namespace GymMembershipSystem.Services;

public interface IAuthService
{
    Task<bool> RegisterAsync(RegisterModel model);
    Task<User?> LoginAsync(LoginModel model);
    Task<User?> GetCurrentUserAsync();
    Task LogoutAsync();
    Task<bool> IsAuthenticatedAsync();
}

