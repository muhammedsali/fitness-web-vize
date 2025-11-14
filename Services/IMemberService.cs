using GymMembershipSystem.Models;

namespace GymMembershipSystem.Services;

public interface IMemberService
{
    Task<MemberInfo?> GetMemberInfoAsync();
    Task SaveMemberInfoAsync(MemberInfo memberInfo);
    Task<bool> HasActiveMembershipAsync();
}

