using Microsoft.AspNetCore.Identity;

public class ApplicationUser : IdentityUser, IApplicationUser
{
    public string Role { get; set; } = "Patient";
}