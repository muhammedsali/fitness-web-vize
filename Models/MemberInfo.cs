namespace GymMembershipSystem.Models;

public class MemberInfo
{
    public string MembershipCode { get; set; } = "";
    public string FullName { get; set; } = "";
    public string Email { get; set; } = "";
    public string Phone { get; set; } = "";
    public string TcKimlikNo { get; set; } = "";
    public string MembershipId { get; set; } = "";
    public string MembershipName { get; set; } = "";
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal Price { get; set; }
}

