namespace AspNetIdentityEndpoints.Dtos;

public record CreateUser(string Email, string Password, string PersonalIdentifier, string Role);