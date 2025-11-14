namespace GymMembershipSystem.Models;

public class User
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Email { get; set; } = "";
    public string Password { get; set; } = "";
    public string FullName { get; set; } = "";
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}

