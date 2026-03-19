namespace Application.members.Inputs;

public record SignInInput
(
    string Email,
    string Password,
    bool RememberMe
);
