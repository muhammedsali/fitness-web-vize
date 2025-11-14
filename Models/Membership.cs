namespace GymMembershipSystem.Models;

public class Membership
{
    public string Id { get; set; } = "";
    public string Name { get; set; } = "";
    public decimal Price { get; set; }
    public int DurationDays { get; set; }
    public List<string> Features { get; set; } = new();
}

