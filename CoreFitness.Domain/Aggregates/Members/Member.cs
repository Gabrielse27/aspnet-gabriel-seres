using CoreFitness.Domain.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreFitness.Domain.Agregates.Members;

public class Member
{
    public string Id { get; set; } = null!;
    public string UserId { get; set; } = null!;

    public int MembershipId { get;  set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? ProfileImageUri { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? ModifiedAt { get; set; }

    public Member()
    {

    }

    private Member(string id, string userId, int membershipId, DateTimeOffset createdAt)
    {
        Id = id;
        UserId = userId;
        MembershipId = membershipId;
        CreatedAt = createdAt;
    }


    public static Member Create(string userId, int membershipId)
    {
        if (string.IsNullOrWhiteSpace(userId))
            throw new ArgumentException("Application User id is required.");

        var member = new Member(
            Guid.NewGuid().ToString(),
            userId,
            membershipId,
            DateTimeOffset.UtcNow
        );

        return member;   
    }

    

    public void UpdateInformation(string firstName, string lastName, string? phoneNumber, string? profileImageUri)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException("First name is required.");

        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("Last name is required.");

        FirstName = firstName.Trim();
        LastName = lastName.Trim();
        PhoneNumber = string.IsNullOrWhiteSpace(phoneNumber) ? null : phoneNumber;
        ProfileImageUri = string.IsNullOrWhiteSpace(profileImageUri) ? null : profileImageUri;
    }

}
