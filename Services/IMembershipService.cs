using GymMembershipSystem.Models;

namespace GymMembershipSystem.Services;

public interface IMembershipService
{
    Task<List<Membership>> GetMembershipsAsync();
    Task<Membership?> GetMembershipByIdAsync(string id);
}

