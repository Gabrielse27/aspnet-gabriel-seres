namespace Application.members.Inputs;

public record UpdateMemberAccountInput
(
    string UserId,
    string FirstName,
    string LastName,
    string? PhoneNumber,
    string? ProfileImageUri
);