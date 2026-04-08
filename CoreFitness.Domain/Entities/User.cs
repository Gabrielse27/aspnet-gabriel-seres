using CoreFitness.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;



namespace CoreFitness.Domain.Identity;

public class User : IdentityUser
{
    public MemberEntity? Member { get; set; }

    public string? FirstName { get; set; }
    public string? LastName { get; set; }

    public string? ProfilePicture { get; set; }
    public override string? PhoneNumber { get; set; }

    public int Age { get; set; }

    public string? ProfileImage { get; set; }

    public static User Create(string email, bool confirmed = true)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email is required");

        var user = new User
        {
            UserName = email,
            Email = email,
            EmailConfirmed = confirmed,

        };

        return user;
    }
}




