using System.Text.Json;
using Microsoft.JSInterop;
using GymMembershipSystem.Models;

namespace GymMembershipSystem.Services;

public class MemberService : IMemberService
{
    private const string StorageKeyPrefix = "gym_member_info_";
    private readonly IJSRuntime _jsRuntime;
    private readonly IAuthService _authService;

    public MemberService(IJSRuntime jsRuntime, IAuthService authService)
    {
        _jsRuntime = jsRuntime;
        _authService = authService;
    }

    private async Task<string> GetStorageKeyAsync()
    {
        var user = await _authService.GetCurrentUserAsync();
        if (user == null)
            return StorageKeyPrefix + "guest";
        
        return StorageKeyPrefix + user.Id;
    }

    public async Task<MemberInfo?> GetMemberInfoAsync()
    {
        try
        {
            var storageKey = await GetStorageKeyAsync();
            var json = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", storageKey);
            if (string.IsNullOrEmpty(json))
                return null;

            return JsonSerializer.Deserialize<MemberInfo>(json);
        }
        catch
        {
            return null;
        }
    }

    public async Task SaveMemberInfoAsync(MemberInfo memberInfo)
    {
        try
        {
            var storageKey = await GetStorageKeyAsync();
            var json = JsonSerializer.Serialize(memberInfo);
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", storageKey, json);
            
            Console.WriteLine($"MemberService: Üyelik bilgisi kaydedildi. Anahtar: {storageKey}");
            
            // Kayıt başarılı olduğunu doğrula
            var saved = await GetMemberInfoAsync();
            if (saved == null)
            {
                throw new Exception("Kayıt doğrulanamadı");
            }
            
            Console.WriteLine($"MemberService: Kayıt doğrulandı. Üyelik Kodu: {saved.MembershipCode}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"MemberService.SaveMemberInfoAsync hatası: {ex.Message}");
            throw;
        }
    }

    public async Task<bool> HasActiveMembershipAsync()
    {
        var memberInfo = await GetMemberInfoAsync();
        if (memberInfo == null)
            return false;

        return DateTime.Now <= memberInfo.EndDate;
    }
}

