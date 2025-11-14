using System.Text.Json;
using Microsoft.JSInterop;
using GymMembershipSystem.Models;

namespace GymMembershipSystem.Services;

public class AuthService : IAuthService
{
    private const string UsersStorageKey = "gym_users";
    private const string CurrentUserKey = "gym_current_user";
    private readonly IJSRuntime _jsRuntime;

    public AuthService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task<bool> RegisterAsync(RegisterModel model)
    {
        try
        {
            var users = await GetAllUsersAsync();
            
            // E-posta kontrolü
            if (users.Any(u => u.Email.Equals(model.Email, StringComparison.OrdinalIgnoreCase)))
            {
                return false; // E-posta zaten kayıtlı
            }

            var newUser = new User
            {
                Email = model.Email,
                Password = model.Password, // Gerçek uygulamada hash'lenmeli
                FullName = model.FullName,
                CreatedAt = DateTime.Now
            };

            users.Add(newUser);
            await SaveAllUsersAsync(users);
            await SetCurrentUserAsync(newUser);
            
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<User?> LoginAsync(LoginModel model)
    {
        try
        {
            var users = await GetAllUsersAsync();
            var user = users.FirstOrDefault(u => 
                u.Email.Equals(model.Email, StringComparison.OrdinalIgnoreCase) && 
                u.Password == model.Password);

            if (user != null)
            {
                await SetCurrentUserAsync(user);
                return user;
            }

            return null;
        }
        catch
        {
            return null;
        }
    }

    public async Task<User?> GetCurrentUserAsync()
    {
        try
        {
            var json = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", CurrentUserKey);
            if (string.IsNullOrEmpty(json))
                return null;

            return JsonSerializer.Deserialize<User>(json);
        }
        catch
        {
            return null;
        }
    }

    public async Task LogoutAsync()
    {
        // Sadece kullanıcı oturum bilgisini temizle, üyelik bilgileri kalıcı olarak saklanır
        await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", CurrentUserKey);
    }

    public async Task<bool> IsAuthenticatedAsync()
    {
        var user = await GetCurrentUserAsync();
        return user != null;
    }

    private async Task<List<User>> GetAllUsersAsync()
    {
        try
        {
            var json = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", UsersStorageKey);
            if (string.IsNullOrEmpty(json))
                return new List<User>();

            return JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();
        }
        catch
        {
            return new List<User>();
        }
    }

    private async Task SaveAllUsersAsync(List<User> users)
    {
        var json = JsonSerializer.Serialize(users);
        await _jsRuntime.InvokeVoidAsync("localStorage.setItem", UsersStorageKey, json);
    }

    private async Task SetCurrentUserAsync(User user)
    {
        var json = JsonSerializer.Serialize(user);
        await _jsRuntime.InvokeVoidAsync("localStorage.setItem", CurrentUserKey, json);
    }
}

