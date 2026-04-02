using CoreFitness.Infrastructure.Persistence.Entities.Members;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity;

public class ApplicationUser : IdentityUser
{
    public MemberEntity? Member { get; set; }

    public string? FirstName { get; set; }
    public string? LastName { get; set; }

    public string? ProfilePicture { get; set; }
    public override string?  PhoneNumber { get; set; }





    public static ApplicationUser Create(string email, bool confirmed = true)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email is required");

        var user = new ApplicationUser
        {
            UserName = email,
            Email = email,
            EmailConfirmed = confirmed,
            
        };

        return user;
    }
}