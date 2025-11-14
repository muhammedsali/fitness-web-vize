using System.Net.Http.Json;
using GymMembershipSystem.Models;

namespace GymMembershipSystem.Services;

public class MembershipService : IMembershipService
{
    private readonly HttpClient _httpClient;
    private List<Membership>? _memberships;

    public MembershipService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Membership>> GetMembershipsAsync()
    {
        if (_memberships == null)
        {
            _memberships = await _httpClient.GetFromJsonAsync<List<Membership>>("memberships.json") 
                ?? new List<Membership>();
        }
        return _memberships;
    }

    public async Task<Membership?> GetMembershipByIdAsync(string id)
    {
        var memberships = await GetMembershipsAsync();
        return memberships.FirstOrDefault(m => m.Id == id);
    }
}

